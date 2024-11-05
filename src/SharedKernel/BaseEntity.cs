using System.ComponentModel.DataAnnotations;

namespace Lia.SharedKernel
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }


        /// <summary>
        /// Determinate is ententy is asigned
        /// </summary>
        /// <returns>true or false is asignade</returns>
        public bool IsTransient()
        {
            return Id == default;
        }
    }
}