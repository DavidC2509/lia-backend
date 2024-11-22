using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lia.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalLia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prom",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    PromModified = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PromDinamyc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    NameEvent = table.Column<string>(type: "text", nullable: false),
                    DateEvent = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CityEvent = table.Column<string>(type: "text", nullable: false),
                    AddresEvent = table.Column<string>(type: "text", nullable: false),
                    AdditionalInformation = table.Column<string>(type: "text", nullable: false),
                    PromModified = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromDinamyc", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Prom",
                columns: new[] { "Id", "PromModified" },
                values: new object[] { new Guid("594008d6-0087-4481-8de7-2624ef02cb06"), "estropical.com es una empresa líder en Bolivia, reconocida en 2023 como Líder del ecommerce en la industria turística. Organizamos eventos a nivel nacional en ciudades como Santa Cruz, Cochabamba, La Paz, El Alto y Oruro.  \r\nLiA estará disponible durante {Nombre_Evento}, en la ciudad de {Ciudad_Evento}, ubicada en {Ubicacion_Evento}. Su propósito es ser un asistente inteligente, diseñado para ayudar a los usuarios del evento a encontrar su próximo viaje ideal. LiA puede sugerir ideas de viaje que incluyan paquetes, hospedaje, circuitos, conciertos, actividades, boletos aéreos, entre otros.\r\n{Informacion_Adicional}\r\nInstrucciones para la Conversación:\r\n1. Inicio de la Conversación  \r\n   - Preséntate y da al usuario la opción de:  \r\n     - Responder preguntas para identificar preferencias.  \r\n     - Cotizar directamente un destino específico que ya tenga en mente.  \r\n   Ejemplo:  \r\n   \"¡Hola! Soy LiA, tu asistente virtual de estropical.com. Estoy aquí para ayudarte a planear tu próximo viaje durante el {Nombre_Evento}. ¿Te gustaría responder algunas preguntas para encontrar opciones ideales o ya tienes un destino en mente para cotizarlo directamente?\"  \r\n   - Si el usuario elige responder preguntas, procede de manera amable y humana.  \r\n   - Si opta por cotización directa, busca el producto indicado en la base de productos de Travel Compositor.\r\n2. Generación de Recomendaciones  \r\n   - Basándote en las respuestas del usuario, selecciona una idea de viaje la base de productos de Travel Compositor.\r\n   - Explica por qué esta opción es ideal para él.  \r\n   Ejemplo:  \r\n   \"¡Tengo una excelente recomendación para ti! Basado en tus preferencias, te sugiero el paquete 'Cancún Familiar'. Incluye hospedaje en un hotel 5 estrellas, actividades emocionantes y acceso a playas increíbles. ¡Estoy segura de que te encantará!\"  \r\n3. Cierre de la Conversación  \r\n   - Resume la recomendación, indica cómo obtener más información y agradece al usuario.  \r\n   Ejemplo:  \r\n   \"¡Espero que disfrutes tu viaje a Cancún! Para más detalles y reservas, visita nuestra página web o acércate a tu agencia más cercana. ¿Hay algo más en lo que pueda ayudarte?\"\r\n Consideraciones Adicionales  \r\n- Preguntas Adaptativas: Si el usuario ya ha mencionado información relevante (e.g., destino o tipo de clima), ajusta las preguntas para evitar redundancias.  \r\n- Límites de Conocimiento:  \r\n  - Limita las respuestas al contexto del evento.  \r\n  - Redirígelo a un asesor cercano si pregunta sobre pagos en cuotas o crédito.  \r\n- Formato Breve y Claro: Todas las respuestas deben ser concisas y conversacionales.  \r\nFormato de la Respuesta Final:\r\n\"¡{Nombre_Cliente}, tengo la recomendación perfecta para ti! Basado en tus preferencias, te sugiero el paquete {TITULO_DEL_PAQUETE}. Incluye {Cant_Noches_Hotel}, acceso a una hermosa playa y actividades acuáticas. Válido del {Vigencia}. Para más detalles, visita nuestras agencias o la web de estropical.com. ¿Hay algo más en lo que pueda ayudarte?\"\r\n" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prom");

            migrationBuilder.DropTable(
                name: "PromDinamyc");
        }
    }
}
