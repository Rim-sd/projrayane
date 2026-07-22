using appTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace appTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly appTestDB _context;

        public HomeController(appTestDB context)
        {
            _context = context;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            var now = DateTime.Today;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var medicaments = _context.Medicament
                .AsNoTracking()
                .Include(m => m.Categorie)
                .Include(m => m.Fournisseurs)
                .ToList();

            var stocks = _context.Stocks
                .AsNoTracking()
                .ToList();

            var totalStockEntree = stocks
                .Where(s => IsEntryType(s.Type))
                .Sum(s => s.Quantite);

            var totalStockSortie = stocks
                .Where(s => IsExitType(s.Type))
                .Sum(s => s.Quantite);

            var totalMovements = totalStockEntree + totalStockSortie;

            var model = new DashboardViewModel
            {
                TotalMedicaments = medicaments.Count,
                TotalStockEntree = totalStockEntree,
                TotalStockSortie = totalStockSortie,
                StockEntreePercentage = totalMovements == 0 ? 0 : Math.Round((decimal)totalStockEntree * 100 / totalMovements, 2),
                StockSortiePercentage = totalMovements == 0 ? 0 : Math.Round((decimal)totalStockSortie * 100 / totalMovements, 2),
                TotalStockValue = medicaments.Sum(m => m.Prix * m.Quantite),
                CurrentMonthLabel = startOfMonth.ToString("MMMM yyyy", CultureInfo.GetCultureInfo("fr-FR")),
                YearlyStock = stocks
                    .GroupBy(s => s.DateStock.Year)
                    .OrderBy(g => g.Key)
                    .Select(g => new YearlyStockPoint
                    {
                        Year = g.Key,
                        Entree = g.Where(s => IsEntryType(s.Type)).Sum(s => s.Quantite),
                        Sortie = g.Where(s => IsExitType(s.Type)).Sum(s => s.Quantite)
                    })
                    .ToList(),
                TopFournisseurs = medicaments
                    .Where(m => m.Fournisseurs != null)
                    .GroupBy(m => m.Fournisseurs!.Nom ?? "Sans fournisseur")
                    .Select(g => new TopFournisseurItem
                    {
                        Nom = g.Key,
                        MedicamentCount = g.Count(),
                        TotalQuantity = g.Sum(m => m.Quantite)
                    })
                    .OrderByDescending(g => g.TotalQuantity)
                    .ThenByDescending(g => g.MedicamentCount)
                    .Take(5)
                    .ToList(),
                MedicamentsByCategory = medicaments
                    .GroupBy(m => m.Categorie != null ? m.Categorie.Nom : "Sans catégorie")
                    .Select(g => new CategoryMetricItem
                    {
                        CategoryName = g.Key,
                        Value = g.Count()
                    })
                    .OrderByDescending(g => g.Value)
                    .ToList(),
                StockByCategory = medicaments
                    .GroupBy(m => m.Categorie != null ? m.Categorie.Nom : "Sans catégorie")
                    .Select(g => new CategoryMetricItem
                    {
                        CategoryName = g.Key,
                        Value = g.Sum(m => m.Quantite)
                    })
                    .OrderByDescending(g => g.Value)
                    .ToList(),
                ExpiringMedicaments = medicaments
                    .Where(m => m.DateExpiration.Date >= startOfMonth && m.DateExpiration.Date <= endOfMonth)
                    .OrderBy(m => m.DateExpiration)
                    .Select(m => new ExpiringMedicamentItem
                    {
                        Code = m.Code ?? string.Empty,
                        Nom = m.Nom ?? string.Empty,
                        Categorie = m.Categorie != null ? m.Categorie.Nom : "Sans catégorie",
                        Quantite = m.Quantite,
                        DateExpiration = m.DateExpiration,
                        RemainingDays = (m.DateExpiration.Date - now).Days
                    })
                    .ToList()
            };

            return View(model);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private static bool IsEntryType(string? type)
        {
            return NormalizeType(type) == "entree";
        }

        private static bool IsExitType(string? type)
        {
            return NormalizeType(type) == "sortie";
        }

        private static string NormalizeType(string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return string.Empty;
            }

            return type
                .Trim()
                .ToLowerInvariant()
                .Replace("é", "e")
                .Replace("è", "e");
        }
    }
}