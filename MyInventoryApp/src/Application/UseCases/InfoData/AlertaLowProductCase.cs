using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.AlertaLowProductCase
{
    public class AlertaLowProductCase
    {
        private IGetInfoRepository _repoInfo { get; set; }

        public AlertaLowProductCase(IGetInfoRepository repo)
        {
            _repoInfo = repo;
        }

        public async Task<List<AlertaLowProductDTO>> ExecuteAsync()
        {
            return await _repoInfo.GetLowProducts();
        }
    }
}
