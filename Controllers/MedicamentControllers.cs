using appTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace appTest.Controllers
{
    public class MedicamentController : Controller
    {
        private readonly appTestDB _context;

        public MedicamentController(appTestDB context)
        {
            _context = context;
        }

        public ActionResult Index(string search, int? FournisseurId, int? CategorieId)
        {
            var Medicament = _context.Medicament
                .Include(m => m.Categorie)
                .Include(m => m.Fournisseurs)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                Medicament = Medicament.Where(p =>
                    p.Nom!.Contains(search) ||
                    p.Code!.Contains(search));
            }

            if (CategorieId.HasValue)
                Medicament = Medicament.Where(m => m.CategorieId == CategorieId);

            if (FournisseurId.HasValue)
                Medicament = Medicament.Where(m => m.FournisseurId == FournisseurId);

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Nom", CategorieId);
            ViewBag.Fournisseurs = new SelectList(_context.Fournisseurs, "Id", "Nom", FournisseurId);

            return View(Medicament.ToList());
        }

        public ActionResult Liste(string search, int? FournisseurId, int? CategorieId)
        {
            var medicaments = _context.Medicament
                .Include(m => m.Categorie)
                .Include(m => m.Fournisseurs)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                medicaments = medicaments.Where(p =>
                    p.Nom!.Contains(search) ||
                    p.Code!.Contains(search));
            }

            if (CategorieId.HasValue)
                medicaments = medicaments.Where(m => m.CategorieId == CategorieId);

            if (FournisseurId.HasValue)
                medicaments = medicaments.Where(m => m.FournisseurId == FournisseurId);

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Nom", CategorieId);
            ViewBag.Fournisseurs = new SelectList(_context.Fournisseurs, "Id", "Nom", FournisseurId);

            return View(medicaments.ToList());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.CategorieId = new SelectList(_context.Categories, "Id", "Nom");
            ViewBag.FournisseurId = new SelectList(_context.Fournisseurs, "Id", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Medicament Medicament)
        {
            ModelState.Remove(nameof(Medicament.Categorie));
            ModelState.Remove(nameof(Medicament.Fournisseurs));

            if (ModelState.IsValid)
            {
                _context.Medicament.Add(Medicament);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategorieId = new SelectList(_context.Categories, "Id", "Nom", Medicament.CategorieId);
            ViewBag.FournisseurId = new SelectList(_context.Fournisseurs, "Id", "Nom", Medicament.FournisseurId);
            return View(Medicament);
        }

        public ActionResult Edit(int id)
        {
            var Medicament = _context.Medicament.SingleOrDefault(m => m.Id == id);
            if (Medicament == null)
                return NotFound();

            ViewBag.CategorieId = new SelectList(_context.Categories, "Id", "Nom", Medicament.CategorieId);
            ViewBag.FournisseurId = new SelectList(_context.Fournisseurs, "Id", "Nom", Medicament.FournisseurId);
            return View(Medicament);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Medicament medicament)
        {
            ModelState.Remove(nameof(Medicament.Categorie));
            ModelState.Remove(nameof(Medicament.Fournisseurs));

            var exist = _context.Medicament.SingleOrDefault(m => m.Id == medicament.Id);
            if (exist == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                exist.Code = medicament.Code;
                exist.Nom = medicament.Nom;
                exist.CategorieId = medicament.CategorieId;
                exist.Prix = medicament.Prix;
                exist.Quantite = medicament.Quantite;
                exist.DateExpiration = medicament.DateExpiration;
                exist.FournisseurId = medicament.FournisseurId;

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategorieId = new SelectList(_context.Categories, "Id", "Nom", medicament.CategorieId);
            ViewBag.FournisseurId = new SelectList(_context.Fournisseurs, "Id", "Nom", medicament.FournisseurId);
            return View(medicament);
        }

        public ActionResult Delete(int id)
        {
            var MedicamentDB = _context.Medicament.SingleOrDefault(m => m.Id == id);
            if (MedicamentDB == null)
                return NotFound();

            _context.Medicament.Remove(MedicamentDB);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
