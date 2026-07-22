using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appTest.Models
{
    [Table("Medicament")]
    public class Medicament
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }

        [Required]
        [MaxLength(150)]
        public string? Nom { get; set; }

        public int CategorieId { get; set; }
        [ForeignKey("CategorieId")]
        public Categorie? Categorie { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Prix { get; set; }

        public int Quantite { get; set; }

        public DateTime DateExpiration { get; set; }

        public int FournisseurId { get; set; }
        [ForeignKey("FournisseurId")]
        public Fournisseurs? Fournisseurs { get; set; }
    }
}
