// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Nhom7_webTourdulich.Models;
// using Microsoft.AspNetCore.Mvc.Rendering;

// namespace Nhom7_webTourdulich.Controllers
// {
//     public class TourController : Controller
//     {
//         private readonly QuanLyTourContext _context;

//         public TourController(QuanLyTourContext context)
//         {
//             _context = context;
//         }

//         // GET: Tour
//         public async Task<IActionResult> Index()
//         {
//             var tours = await _context.Tours
//                 .Include(t => t.MaLoaiTourNavigation)  // Include related LoaiTour
//                 .Include(t => t.MaGiaTourNavigation)  // Include related GiaTour
//                 .ToListAsync();
//             return View(tours);
//         }

//         // GET: Tour/Create
// // GET: Tour/Create
//         public IActionResult Create()
//         {
//             // Assuming your _context.GiaTours returns a list of available price options
//             ViewData["GiaTourList"] = new SelectList(_context.GiaTours, "MaGiaTour", "Gia");
//             return View();
//         }


//         // POST: Tour/Create
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create([Bind("MaTour,Ten,MaLoaiTour,MaGiaTour,SoNgay,HinhAnh")] Tour tour)
//         {
//             if (ModelState.IsValid)
//             {
//                 _context.Add(tour);
//                 await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }

//             ViewData["MaLoaiTour"] = new SelectList(_context.LoaiTours, "MaLoaiTour", "TenLoaiTour", tour.MaLoaiTour);
//             ViewData["MaGiaTour"] = new SelectList(_context.GiaTours, "MaGiaTour", "Gia", tour.MaGiaTour);
//             return View(tour);
//         }

//         // GET: Tour/Edit/5
//         public async Task<IActionResult> Edit(string id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var tour = await _context.Tours.FindAsync(id);
//             if (tour == null)
//             {
//                 return NotFound();
//             }

//             ViewData["MaLoaiTour"] = new SelectList(_context.LoaiTours, "MaLoaiTour", "TenLoaiTour", tour.MaLoaiTour);
//             ViewData["MaGiaTour"] = new SelectList(_context.GiaTours, "MaGiaTour", "Gia", tour.MaGiaTour);
//             return View(tour);
//         }

//         // POST: Tour/Edit/5
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(string id, [Bind("MaTour,Ten,MaLoaiTour,MaGiaTour,SoNgay,HinhAnh")] Tour tour)
//         {
//             if (id != tour.MaTour)
//             {
//                 return NotFound();
//             }

//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _context.Update(tour);
//                     await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!TourExists(tour.MaTour))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }

//             ViewData["MaLoaiTour"] = new SelectList(_context.LoaiTours, "MaLoaiTour", "TenLoaiTour", tour.MaLoaiTour);
//             ViewData["MaGiaTour"] = new SelectList(_context.GiaTours, "MaGiaTour", "Gia", tour.MaGiaTour);
//             return View(tour);
//         }

//         // GET: Tour/Delete/5
//         public async Task<IActionResult> Delete(string id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }

//             var tour = await _context.Tours
//                 .Include(t => t.MaLoaiTourNavigation)
//                 .Include(t => t.MaGiaTourNavigation)
//                 .FirstOrDefaultAsync(m => m.MaTour == id);
//             if (tour == null)
//             {
//                 return NotFound();
//             }

//             return View(tour);
//         }

//         // POST: Tour/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(string id)
//         {
//             var tour = await _context.Tours.FindAsync(id);
//             _context.Tours.Remove(tour);
//             await _context.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }

//         private bool TourExists(string id)
//         {
//             return _context.Tours.Any(e => e.MaTour == id);
//         }
//     }
// }
