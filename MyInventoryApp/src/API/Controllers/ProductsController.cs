using Microsoft.AspNetCore.Mvc;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.UseCases.Categories;
using MyInventoryApp.src.Application.UseCases.Products;
using MyInventoryApp.src.Domain.Entities;

namespace MyInventoryApp.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly CreateProductUseCase _useCase;
        private readonly ListProduct _useCaseList;
        private readonly GetProductsUseCase _useGetProduct;
        private readonly IncreaseStockUseCase _useIncreaseStock;
        private readonly DecreaseStockUseCase _useDecreaseStock;
        public ProductsController(
            CreateProductUseCase useCase,
            ListProduct useCaseList,
            GetProductsUseCase useGetProduct,
            IncreaseStockUseCase useIncreaseStock,
            DecreaseStockUseCase useDecreaseStock
            )
        {
            _useCase = useCase;
            _useCaseList = useCaseList;
            _useGetProduct = useGetProduct;
            _useIncreaseStock = useIncreaseStock;
            _useDecreaseStock = useDecreaseStock;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoDTO dto)
        {
            var result = await _useCase.Execute(dto);
            if(!result.IsSuccess) 
                return NotFound(new {message = result.Error});

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _useCaseList.ExecuteAsync();
            if(!result.IsSuccess)
                return NotFound(new {message = result.Error});

            return Ok(result);
        }
        [HttpGet]
        [Route("ById")]
        public async Task<IActionResult> GetProductoById(Guid Id)
        {
            var result = await _useGetProduct.GetProducts(Id);

            if(!result.IsSuccess)
                return NotFound(new {message = result.Error});

            return Ok(result);
        }

        [HttpPost]
        [Route("Increase")]
        public async Task<IActionResult> IncreaseProduct([FromBody] ProductRequest request)
        {
            var result = await _useIncreaseStock.ExecuteAsync(request.ProductId, request.Quantity);
            if(!result.IsSuccess)
                return NotFound(new {message = result.Error});

            return Ok(result);
        }

        [HttpPost]
        [Route("Decrease")]
        public async Task<IActionResult> DecreaseProduct([FromBody] ProductRequest request)
        {
            Console.WriteLine("ProductoId: " + request.ProductId);

            var result = await _useDecreaseStock.ExecuteAsync(request.ProductId, request.Quantity);
            if (!result.IsSuccess)
                return NotFound(new { message = result.Error });

            return Ok(result);
        }
    }
}
