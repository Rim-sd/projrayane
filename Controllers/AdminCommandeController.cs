using appTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appTest.Controllers
{
    public class AdminCommandeController : Controller
    {
        private readonly appTestDB _context;

        public AdminCommandeController(appTestDB context)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accepter(int id)
        {
            return MettreAJourStatut(id, "accepte");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EnAttente(int id)
        {
            return MettreAJourStatut(id, "en attente");
        }

        private IActionResult MettreAJourStatut(int id, string statut)
        {
            var commande = _context.Commandes.SingleOrDefault(c => c.Id == id);
            if (commande == null)
                return NotFound();

            commande.Statut = statut;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
