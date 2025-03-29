using Nhom7_webTourdulich.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nhom7_webTourdulich.Repositories
{
    public interface ITourRepository
    {
        // Các phương thức hiện tại
        Task<IEnumerable<Tour>> GetAllAsync();
        Task<Tour> GetByIdAsync(int id);
        Task AddAsync(Tour tour);
        Task UpdateAsync(Tour tour);
        Task DeleteAsync(int id);
    Task AddImageAsync(TourImage tourImage);

        // Thêm các phương thức mới
        Task<IEnumerable<LoaiTour>> GetLoaiTours();   // Lấy danh sách LoaiTour
        Task<IEnumerable<GiaTour>> GetGiaTours();     // Lấy danh sách GiaTour
        Task<IEnumerable<DiemDen>> GetDiemDens();     // Lấy danh sách DiemDen
        Task<bool> ExistsAsync(int id);               // Kiểm tra xem tour có tồn tại không
        Task<IEnumerable<Tour>> GetAllToursWithDetails();  // Phương thức này cần được khai báo ở đây

    }
}
