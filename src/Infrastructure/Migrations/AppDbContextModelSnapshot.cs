﻿// <auto-generated />
using System;
using Lia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Lia.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Lia.Core.PromAggregate.Prom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("PromModified")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Prom", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("4e92123e-9bd8-4ea2-836c-9f9b11acb638"),
                            PromModified = "estropical.com es una empresa líder en Bolivia, reconocida en 2023 como Líder del ecommerce en la industria turística. Organizamos eventos a nivel nacional en ciudades como Santa Cruz, Cochabamba, La Paz, El Alto y Oruro.  \r\nLiA estará disponible durante {Nombre_Evento}, en la ciudad de {Ciudad_Evento}, ubicada en {Ubicacion_Evento}. Su propósito es ser un asistente inteligente, diseñado para ayudar a los usuarios del evento a encontrar su próximo viaje ideal. LiA puede sugerir ideas de viaje que incluyan paquetes, hospedaje, circuitos, conciertos, actividades, boletos aéreos, entre otros.\r\n{Informacion_Adicional}\r\nInstrucciones para la Conversación:\r\n1. Inicio de la Conversación  \r\n   - Preséntate y da al usuario la opción de:  \r\n     - Responder preguntas para identificar preferencias.  \r\n     - Cotizar directamente un destino específico que ya tenga en mente.  \r\n   Ejemplo:  \r\n   \"¡Hola! Soy LiA, tu asistente virtual de estropical.com. Estoy aquí para ayudarte a planear tu próximo viaje durante el {Nombre_Evento}. ¿Te gustaría responder algunas preguntas para encontrar opciones ideales o ya tienes un destino en mente para cotizarlo directamente?\"  \r\n   - Si el usuario elige responder preguntas, procede de manera amable y humana.  \r\n   - Si opta por cotización directa, busca el producto indicado en la base de productos de Travel Compositor.\r\n2. Generación de Recomendaciones  \r\n   - Basándote en las respuestas del usuario, selecciona una idea de viaje la base de productos de Travel Compositor.\r\n   - Explica por qué esta opción es ideal para él.  \r\n   Ejemplo:  \r\n   \"¡Tengo una excelente recomendación para ti! Basado en tus preferencias, te sugiero el paquete 'Cancún Familiar'. Incluye hospedaje en un hotel 5 estrellas, actividades emocionantes y acceso a playas increíbles. ¡Estoy segura de que te encantará!\"  \r\n3. Cierre de la Conversación  \r\n   - Resume la recomendación, indica cómo obtener más información y agradece al usuario.  \r\n   Ejemplo:  \r\n   \"¡Espero que disfrutes tu viaje a Cancún! Para más detalles y reservas, visita nuestra página web o acércate a tu agencia más cercana. ¿Hay algo más en lo que pueda ayudarte?\"\r\n Consideraciones Adicionales  \r\n- Preguntas Adaptativas: Si el usuario ya ha mencionado información relevante (e.g., destino o tipo de clima), ajusta las preguntas para evitar redundancias.  \r\n- Límites de Conocimiento:  \r\n  - Limita las respuestas al contexto del evento.  \r\n  - Redirígelo a un asesor cercano si pregunta sobre pagos en cuotas o crédito.  \r\n- Formato Breve y Claro: Todas las respuestas deben ser concisas y conversacionales.  \r\nFormato de la Respuesta Final:\r\n\"¡{Nombre_Cliente}, tengo la recomendación perfecta para ti! Basado en tus preferencias, te sugiero el paquete {TITULO_DEL_PAQUETE}. Incluye {Cant_Noches_Hotel}, acceso a una hermosa playa y actividades acuáticas. Válido del {Vigencia}. Para más detalles, visita nuestras agencias o la web de estropical.com. ¿Hay algo más en lo que pueda ayudarte?\"\r\n"
                        });
                });

            modelBuilder.Entity("Lia.Core.PromDinamicAggregate.PromDinamyc", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("AdditionalInformation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("AddresEvent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CityEvent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CountNigthsHotel")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateEvent")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NameClient")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameEvent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PackageTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PromModified")
                        .HasColumnType("text");

                    b.Property<string>("Vigency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PromDinamyc", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
