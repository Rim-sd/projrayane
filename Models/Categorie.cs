using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appTest.Models
{
    [Table("Categorie")]
    public class Categorie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nom { get; set; } = string.Empty;
    }
}