using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.Categories;
using MyInventoryApp.src.Application.UseCases.Notify;
using MyInventoryApp.src.Application.UseCases.Products;

namespace MyInventoryApp.src.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseController
    {

        private readonly CreateProductUseCase _useCase;
        private readonly ListProduct _useCaseList;
        private readonly GetProductsUseCase _useGetProduct;
        private readonly IncreaseStockUseCase _useIncreaseStock;
        private readonly DecreaseStockUseCase _useDecreaseStock;
        private readonly NotifyLowStockUseCase _notifyLowStockUseCase;
        public ProductsController(
            CreateProductUseCase useCase,
            ListProduct useCaseList,
            GetProductsUseCase useGetProduct,
            IncreaseStockUseCase useIncreaseStock,
            DecreaseStockUseCase useDecreaseStock,
            NotifyLowStockUseCase notifyLowStockUseCase
            )
        {
            _useCase = useCase;
            _useCaseList = useCaseList;
            _useGetProduct = useGetProduct;
            _useIncreaseStock = useIncreaseStock;
            _useDecreaseStock = useDecreaseStock;
            _notifyLowStockUseCase = notifyLowStockUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO dto)
        {
            var result = await _useCase.Execute(dto);
           return FromCreated(result, nameof(GetProducts));
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _useCaseList.ExecuteAsync();
            return FromResult(result);
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> GetProductoById(Guid Id)
        {
            var result = await _useGetProduct.Execute(Id);

           return FromResult(result);
        }


        [HttpPost]
        [Route("increase")]
        public async Task<IActionResult> IncreaseProduct([FromBody] ProductRequest request)
        {
            var result = await _useIncreaseStock.ExecuteAsync(request.ProductId, request.Quantity);
            return FromResult(result);
        }

        [HttpPost]
        [Route("decrease")]
        public async Task<IActionResult> DecreaseProduct([FromBody] ProductRequest request)
        {

            var result = await _useDecreaseStock.ExecuteAsync(request.ProductId, request.Quantity);
            return FromResult(result);
        }
    }
}
