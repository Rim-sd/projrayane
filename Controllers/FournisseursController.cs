using appTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace appTest.Controllers
{
    public class FournisseursController : Controller
    {
        private readonly appTestDB _context;
        public FournisseursController(appTestDB context)
        {
            _context = context;
        }
        // GET: FournisseursController
        public IActionResult Index()
        {
            var fournisseurs = _context.Fournisseurs.ToList();
            return View(fournisseurs);
        }

        // GET: FournisseursController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: CategorieController/Create
        public ActionResult Create()
        {
            //tolist;
            return View();
        }
        // POST: FournisseursController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fournisseurs Fournisseurs)
        {
            if (ModelState.IsValid)
            {
                _context.Fournisseurs.Add(Fournisseurs);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Fournisseurs);

        }

        // GET: FournisseursController/Edit/5
        public ActionResult Edit(int id)
        {
            var FournisseursDB = _context.Fournisseurs.SingleOrDefault(Fournisseurs => Fournisseurs.Id == id);
            if (FournisseursDB == null)
            {
                return NotFound();
            }
            return View(FournisseursDB);
        }

        // POST: FournisseursController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fournisseurs Fournisseurs)
        {

            var exist = _context.Fournisseurs.SingleOrDefault(Fournisseur => Fournisseur.Id == Fournisseurs.Id);
            if (exist == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                exist.Id = Fournisseurs.Id;
                exist.Nom = Fournisseurs.Nom;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(Fournisseurs);
        }


        // GET: FournisseursController/Delete/5
        public ActionResult Delete(int id)
        {
            var produitDB = _context.Fournisseurs.SingleOrDefault(Fournisseurs => Fournisseurs.Id == id);
            if (produitDB == null)
            {
                return NotFound();
            }
            _context.Fournisseurs.Remove(produitDB);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


