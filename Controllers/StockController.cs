using appTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections;
namespace appTest.Controllers
{
    public class StockController : Controller
    {
        private readonly appTestDB _context;
        public StockController(appTestDB context)
        {
            _context = context;
        }
        // GET: StockController
        public ActionResult Index(string search,int? MedicamentId )
        {
            var stocks = _context.Stocks.Include(m => m.Medicament).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                stocks = stocks.Where(p => p.Medicament.Nom.Contains(search));
            }
            if (MedicamentId.HasValue)
            {
                stocks = stocks.Where(m => m.MedicamentId == MedicamentId.Value);
            }

            ViewBag.Stocks = new SelectList(_context.Stocks, "Id", "Nom", "MedicamentId");
            return View(stocks.ToList());
        }

        // GET: StockController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StockController/Create
        public ActionResult Create()
        {
            ViewBag.MedicamentId = new SelectList(_context.Medicament, "Id", "Nom");
            return View();
        }

        // POST: StockController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Stock stock)
        {
            
                if (ModelState.IsValid)
                {
                    _context.Stocks.Add(stock);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            ViewBag.MedicamentId = new SelectList(_context.Medicament, "Id", "Nom", stock.MedicamentId);
            return View(stock);         
        }

        // GET: StockController/Edit/5
        public ActionResult Edit(int id)
        {
            var exist = _context.Stocks.SingleOrDefault(Stock => Stock.Id == id);
            if (exist == null)
            {
                return NotFound();
            }
            ViewBag.MedicamentId = new SelectList(_context.Medicament, "Id", "Nom", exist.MedicamentId);

            return View(exist);
        }

        // POST: StockController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Stock stock)
        {
            var exist = _context.Stocks.SingleOrDefault(s => s.Id == stock.Id);
            if (exist==null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                exist.Id = stock.Id;
                exist.MedicamentId = stock.MedicamentId;
                exist.Type = stock.Type;
                exist.Quantite = stock.Quantite;
                exist.DateStock = stock.DateStock;
                exist.Motif = stock.Motif;
                exist.Medicament = stock.Medicament;
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MedicamentId = new SelectList(_context.Medicament,"Id", "Nom", stock.MedicamentId);
            return View(stock);
        }
        

        // GET: StockController/Delete/5
        public ActionResult Delete(int id)
        {
            var exist = _context.Stocks.SingleOrDefault(Stock => Stock.Id == id);
            if (exist == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(exist);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
