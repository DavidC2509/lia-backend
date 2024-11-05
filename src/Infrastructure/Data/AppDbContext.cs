using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Lia.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IMediator _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
           : base(options)
        {
            _mediator = mediator;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }


    }
}