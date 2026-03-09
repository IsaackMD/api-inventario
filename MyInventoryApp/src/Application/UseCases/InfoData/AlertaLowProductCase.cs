using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Infraestructure.Persistence.Repositories;

namespace MyInventoryApp.src.Application.UseCases.AlertaLowProductCase
{
    public class AlertaLowProductCase
    {
        private GetInfoRepository _repoInfo { get; set; }

        public AlertaLowProductCase(GetInfoRepository repo)
        {
            _repoInfo = repo;
        }

        public async Task<List<AlertaLowProductDTO>> ExecuteAsync()
        {
            return await _repoInfo.GetLowProducts();
        }
    }
}
