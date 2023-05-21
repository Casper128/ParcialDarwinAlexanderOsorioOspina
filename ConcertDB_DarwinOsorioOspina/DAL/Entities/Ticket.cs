using System.ComponentModel.DataAnnotations;

namespace ConcertDB_DarwinOsorioOspina.DAL.Entity
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "EntranceGate")]
        [MaxLength(100, ErrorMessage = "The {0} field must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string? EntranceGate { get; set; }

        [Display(Name = "IsUsed")]
        [MaxLength(100, ErrorMessage = "The {0} field must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public Boolean IsUsed { get; set; }

        [Display(Name = "IsUsed")]
        [MaxLength(100, ErrorMessage = "The {0} field must have a maximum of {1} characters.")]
        [Required(ErrorMessage = "The field {0} is required.")]
        public string? UseDate { get; set; }

    }
}
