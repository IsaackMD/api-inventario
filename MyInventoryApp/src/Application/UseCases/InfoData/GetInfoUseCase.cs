using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.InfoData
{
    public class GetInfoUseCase
    {
        private IGetInfoRepository _repoInfo { get; set; }

        public GetInfoUseCase(IGetInfoRepository repo)
        {
            _repoInfo = repo;
        }

        public async Task<Result<DataDTO>> ExecuteAsync()
        {
            var results = await _repoInfo.GetCountDashboard();

            if (results == null)
            {
                return Result<DataDTO>.Failure("Datos no encontrados");
            }

            return Result<DataDTO>.Success(results);
        }
    }
}
