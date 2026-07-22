using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appTest.Models
{
    [Table("Commande")]
    public class Commande
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string CodeCommande { get; set; } = string.Empty;

        [Required]
        public int MedicamentId { get; set; }

        [ForeignKey(nameof(MedicamentId))]
        public Medicament? Medicament { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La quantité doit être supérieure à 0.")]
        public int Quantite { get; set; }

        [Required]
        public DateOnly DateCommande { get; set; }

        [MaxLength(50)]
        public string? Statut { get; set; }
    }
}
