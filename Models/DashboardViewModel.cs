namespace appTest.Models
{
    public class DashboardViewModel
    {
        public int TotalMedicaments { get; set; }
        public int TotalStockEntree { get; set; }
        public int TotalStockSortie { get; set; }
        public decimal StockEntreePercentage { get; set; }
        public decimal StockSortiePercentage { get; set; }
        public decimal TotalStockValue { get; set; }
        public string CurrentMonthLabel { get; set; } = string.Empty;
        public List<YearlyStockPoint> YearlyStock { get; set; } = new();
        public List<TopFournisseurItem> TopFournisseurs { get; set; } = new();
        public List<CategoryMetricItem> MedicamentsByCategory { get; set; } = new();
        public List<CategoryMetricItem> StockByCategory { get; set; } = new();
        public List<ExpiringMedicamentItem> ExpiringMedicaments { get; set; } = new();

        public string CurrentMonthTitle =>
            string.IsNullOrWhiteSpace(CurrentMonthLabel)
                ? "Médicaments proches de l'expiration"
                : $"Médicaments qui expirent en {CurrentMonthLabel}";
    }

    public class YearlyStockPoint
    {
        public int Year { get; set; }
        public int Entree { get; set; }
        public int Sortie { get; set; }
    }

    public class TopFournisseurItem
    {
        public string Nom { get; set; } = string.Empty;
        public int MedicamentCount { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class CategoryMetricItem
    {
        public string CategoryName { get; set; } = string.Empty;
        public int Value { get; set; }
    }

    public class ExpiringMedicamentItem
    {
        public string Code { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Categorie { get; set; } = string.Empty;
        public int Quantite { get; set; }
        public DateTime DateExpiration { get; set; }
        public int RemainingDays { get; set; }
    }
}
