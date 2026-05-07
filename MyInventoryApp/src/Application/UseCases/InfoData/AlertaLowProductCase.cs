using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
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

        public async Task<Result<IEnumerable<AlertLowProductDTO>>> ExecuteAsync()
        {
            var result = await _repoInfo.GetLowProducts();

            if(!result.Any())
                return Result<IEnumerable<AlertLowProductDTO>>.Failure("Producto de lote no encontrado.");

            return Result<IEnumerable<AlertLowProductDTO>>.Success(result);
        }
    }
}
