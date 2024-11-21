using Autofac;
using Lia.Core.Interface;
using Lia.Core.PromAggregate;
using Lia.Infrastructure.Data;
using Lia.Infrastructure.Interface;
using Lia.SharedKernel.Interface;
using MediatR;
using MediatR.Pipeline;
using System.Reflection;
using Module = Autofac.Module;

namespace Lia.Infrastructure
{
    public class DefaultInfrastructureModule : Module
    {
        private readonly List<Assembly> _assemblies = new List<Assembly>();
        private readonly bool _isDevelopment = false;

        public DefaultInfrastructureModule(bool isDevelopment, Assembly? callingAssembly = null)
        {
            _isDevelopment = isDevelopment;
            // Obtiene referencias a los ensamblados de la aplicación.
            var coreAssembly =
              Assembly.GetAssembly(typeof(Prom));


            var infrastructureAssembly = Assembly.GetAssembly(typeof(StartupSetup));

            // Agrega los ensamblados a la lista de ensamblados.
            if (coreAssembly != null)
            {
                _assemblies.Add(coreAssembly);
            }

            if (infrastructureAssembly != null)
            {
                _assemblies.Add(infrastructureAssembly);
            }

            if (callingAssembly != null)
            {
                _assemblies.Add(callingAssembly);
            }
        }

        // Método que se llama cuando se carga este módulo en Autofac.
        protected override void Load(ContainerBuilder builder)
        {
            // Llama al método para registrar dependencias comunes.
            RegisterCommonDependencies(builder);
        }

        // Método para registrar dependencias comunes.
        private void RegisterCommonDependencies(ContainerBuilder builder)
        {
            // Registra un tipo genérico EfRepository<> como IRepository<> e IReadRepository<>.
            builder.RegisterGeneric(typeof(EfRepository<>))
                  .As(typeof(IRepository<>))
                  .As(typeof(IReadRepository<>))
                  .InstancePerLifetimeScope();

            // Registra el tipo Mediator como IMediator.
            builder.RegisterType<Mediator>()
                 .As<IMediator>()
                 .InstancePerLifetimeScope();


            // Registra el tipo Mediator como IMediator.
            builder.RegisterType<VirtualAssistant>()
                 .As<IVirtualAssistant>()
                 .InstancePerLifetimeScope();

            builder.RegisterType<TravelCService>()
              .As<ITravelCServices>()
              .InstancePerLifetimeScope();



            // Registra un ServiceFactory para resolver tipos desde el contenedor.
            builder.Register<Func<Type, object>>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return type => componentContext.Resolve(type);
            });

            // Define una serie de tipos abiertos de MediatR para buscar e registrar.
            var mediatrOpenTypes = new[]
               {
                  typeof(IRequestHandler<,>),
                  typeof(IRequestExceptionHandler<,,>),
                  typeof(IRequestExceptionAction<,>),
                  typeof(INotificationHandler<>),
                };

            // Itera sobre los tipos abiertos de MediatR y los registra en el contenedor.
            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                  .RegisterAssemblyTypes(_assemblies.ToArray())
                  .AsClosedTypesOf(mediatrOpenType)
                  .AsImplementedInterfaces();
            }
        }
    }
}