using Microsoft.EntityFrameworkCore;
using Nhom7_webTourdulich.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Repositories
{
  public class EFTourRepository : ITourRepository
{
    private readonly QuanLyTourContext _context;

    public EFTourRepository(QuanLyTourContext context)
    {
        _context = context;
    }

    // Implementation of the GetAllAsync method
    public async Task<IEnumerable<Tour>> GetAllAsync()
    {
        return await _context.Tours
            .Include(t => t.MaLoaiTourNavigation)
            .Include(t => t.MaGiaTourNavigation)
            .Include(t => t.MaDiemDenNavigation)
            .Include(t => t.TourImages) // Ensure images are loaded with the tours
            .ToListAsync();
    }

    public async Task AddAsync(Tour tour)
    {
        _context.Tours.Add(tour);
        await _context.SaveChangesAsync();
    }
public async Task AddImageAsync(TourImage tourImage)
{
    _context.TourImages.Add(tourImage);  // Add the image to the TourImages table
    await _context.SaveChangesAsync();    // Save changes to the database
}


    public async Task<Tour> GetByIdAsync(int id)
    {
        return await _context.Tours
            .Include(t => t.TourImages) // Ensure images are loaded with the tour
            .FirstOrDefaultAsync(t => t.MaTour == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Tours.AnyAsync(e => e.MaTour == id);
    }

    public async Task<IEnumerable<Tour>> GetAllToursWithDetails()
    {
        return await _context.Tours
            .Include(t => t.MaLoaiTourNavigation)
            .Include(t => t.MaGiaTourNavigation)
            .Include(t => t.MaDiemDenNavigation)
            .Include(t => t.TourImages)  // Ensure images are loaded with the tours
            .ToListAsync();
    }

    public async Task<IEnumerable<LoaiTour>> GetLoaiTours()
    {
        return await _context.LoaiTours.ToListAsync();
    }

    public async Task<IEnumerable<GiaTour>> GetGiaTours()
    {
        return await _context.GiaTours.ToListAsync();
    }

    public async Task<IEnumerable<DiemDen>> GetDiemDens()
    {
        return await _context.DiemDens.ToListAsync();
    }

    public async Task UpdateAsync(Tour tour)
    {
        _context.Tours.Update(tour);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tour = await _context.Tours.FindAsync(id);
        if (tour != null)
        {
            _context.Tours.Remove(tour);
            await _context.SaveChangesAsync();
        }
    }
}
}