using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Infraestructure.Persistence.Repositories;

namespace MyInventoryApp.src.Application.UseCases.InfoData
{
    public class GetInfoUseCase
    {
        private GetInfoRepository _repoInfo { get; set; }

        public GetInfoUseCase(GetInfoRepository repo)
        {
            _repoInfo = repo;
        }

        public async Task<DataDTO> ExecuteAsync()
        {
            return await _repoInfo.GetCountDashboard();
        }
    }
}
