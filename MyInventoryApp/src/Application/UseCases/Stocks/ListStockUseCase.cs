using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;

namespace MyInventoryApp.src.Application.UseCases.Stocks
{
    public class ListStockUseCase
    {
        private readonly IStockMovementRepository _repository;
        private readonly IMapper _mapper;

        public ListStockUseCase(
            IStockMovementRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<StockDTO>>> ExecuteAsync()
        {
            var stocks = await _repository.GetAllAsync();
            if(!stocks.Any())
                return Result<IEnumerable<StockDTO>>.Success([]);

            return Result<IEnumerable<StockDTO>>.Success(stocks);
        }

        public async Task<Result<StockDTO>> ExecuteSingle(Guid Id)
        {
            var stock = await _repository.GetStockByProduct(Id);
            if(stock == null)
                return Result<StockDTO>.Failure("Stock no encontrado");

            var stockDTO = _mapper.Map<StockDTO>(stock);
            return Result<StockDTO>.Success(stockDTO);
        }

        public async Task<Result<List<StockDTO>>> ExecuteLastMovements()
        {
            var stocks = await _repository.GetLastMovements();
            if (!stocks.Any())
                return Result<List<StockDTO>>.Failure("No hay movimientos de stock");
            var stockDTOs = _mapper.Map<List<StockDTO>>(stocks);
            return Result<List<StockDTO>>.Success(stockDTOs);
        }

    }
}
