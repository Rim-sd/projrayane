using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appTest.Models
{
    [Table("Fournisseurs")]
    public class Fournisseurs
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Nom { get; set; }
    }
}