using appTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace appTest.Controllers
{
    public class CategorieController : Controller
       
    {
        private readonly appTestDB _context;
        public CategorieController(appTestDB context)
        {
            _context = context;
        }


        // GET: CategorieController
        public IActionResult Index()
        {
            var categories= _context.Categories.ToList();
            return View(categories);
        }

        // GET: CategorieController/Details/5
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

        // POST: CategorieController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //dyl sécurité pour éviter les attaques CSRF 
        public ActionResult Create(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add (categorie);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categorie);
        }
        // GET: CategorieController/Edit/5
        public ActionResult Edit(int id)
   {
          {
              var exist = _context.Categories.SingleOrDefault(Categorie => Categorie.Id == id);
                if (exist == null)
                {
                    return NotFound();
                }
               return View(exist);
           }    
 
        }

        // POST: CategorieController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categorie categorie)
        {
            var exist = _context.Categories.SingleOrDefault(Categorie => Categorie.Id == categorie.Id);
            if (ModelState.IsValid)
            {

                exist.Id = categorie.Id;
                exist.Nom= categorie.Nom;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categorie);
        }
        
        public ActionResult Delete(int id)
        {
            var appTestDB = _context.Categories.SingleOrDefault(Categorie => Categorie.Id == id);
            if (appTestDB == null) {
                return NotFound();
            }
            _context.Categories.Remove(appTestDB);
            _context.SaveChanges();

            return RedirectToAction("Index");

            }
        }
}