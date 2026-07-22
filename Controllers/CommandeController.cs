using appTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace appTest.Controllers
{
    public class CommandeController : Controller
    {
        private readonly appTestDB _context;

        public CommandeController(appTestDB context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var commandes = _context.Commandes
                .Include(c => c.Medicament)
                .OrderByDescending(c => c.DateCommande)
                .ToList();

            return View(commandes);
        }

        public IActionResult Create()
        {
            ChargerMedicaments();
            return View(new Commande
            {
                DateCommande = DateOnly.FromDateTime(DateTime.Today),
                CodeCommande = GenererCodeCommande()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Commande commande)
        {
            ModelState.Remove(nameof(Commande.Medicament));
            ModelState.Remove(nameof(Commande.Statut));
            commande.Statut = null;

            if (ModelState.IsValid)
            {
                _context.Commandes.Add(commande);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ChargerMedicaments(commande.MedicamentId);
            return View(commande);
        }

        public IActionResult Edit(int id)
        {
            var commande = _context.Commandes.SingleOrDefault(c => c.Id == id);
            if (commande == null)
                return NotFound();

            ChargerMedicaments(commande.MedicamentId);
            return View(commande);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Commande commande)
        {
            ModelState.Remove(nameof(Commande.Medicament));
            ModelState.Remove(nameof(Commande.Statut));

            var exist = _context.Commandes.SingleOrDefault(c => c.Id == commande.Id);
            if (exist == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                exist.CodeCommande = commande.CodeCommande;
                exist.MedicamentId = commande.MedicamentId;
                exist.Quantite = commande.Quantite;
                exist.DateCommande = commande.DateCommande;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ChargerMedicaments(commande.MedicamentId);
            return View(commande);
        }

        public IActionResult Delete(int id)
        {
            var commande = _context.Commandes.SingleOrDefault(c => c.Id == id);
            if (commande == null)
                return NotFound();

            _context.Commandes.Remove(commande);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private void ChargerMedicaments(int? selectedId = null)
        {
            ViewBag.MedicamentId = new SelectList(_context.Medicament, "Id", "Nom", selectedId);
        }

        private string GenererCodeCommande()
        {
            return "CMD-" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}
