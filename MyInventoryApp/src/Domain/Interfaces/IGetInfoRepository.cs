using MyInventoryApp.src.Application.DTOs;

namespace MyInventoryApp.src.Domain.Interfaces
{
    public interface IGetInfoRepository
    {
        Task<DataDTO> GetCountDashboard();
        Task<List<AlertLowProductDTO>> GetLowProducts();
    }
}
