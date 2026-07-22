using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appTest.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        [Required]
        public int MedicamentId { get; set; }
        [ForeignKey("MedicamentId")]

        [Required]
        [MaxLength(150)]
        public string? Type { get; set; } = string.Empty;
        
        public int Quantite { get; set; }
        [Required]
        public  DateOnly DateStock { get; set; }
        [MaxLength(250)]
        public string? Motif {  get; set;}
        [MaxLength(250)]
        public Medicament? Medicament { get; set; }
        
    }
}