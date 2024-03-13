using AutoMapper;
using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.DiscountDto;
using EntitiesLayer.DTOs.ProductDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet("getallproducts")]
        public ActionResult<List<Product>> GetAllProducts()
        {
            var products = _productService.TGetListAll();
            if(products != null) {
                return Ok(products);
            }
            return NotFound(ErrorMessages<Product>.NoItemFound);
        }

        [HttpGet("getproductswithcategoryname")]
        public ActionResult<List<ResultProductWithCategoryDto>> GetAllProductsWithCategoryName()
        {
            var products = _productService.TGetProductsWithCategoryName();
            if (products != null)
            {
                List<ResultProductWithCategoryDto> productWithCategories = new List<ResultProductWithCategoryDto>();
                foreach (var product in products)
                {
                    ResultProductWithCategoryDto resultProductWithCategoryDto = new ResultProductWithCategoryDto()
                    {
                        Description = product.Description,
                        CategoryName = product.Category.CategoryName,
                        ImageUrl = product.ImageUrl,
                        ProductID = product.ProductID,
                        Price = product.Price,
                        ProductName = product.ProductName,
                        ProductStatus = product.ProductStatus
                    };
                    productWithCategories.Add(resultProductWithCategoryDto);
                }
               
                return Ok(productWithCategories);
            }
            return NotFound(ErrorMessages<Product>.NoItemFound);
        }

        [HttpGet("getproductbyid/{id}")]
        public ActionResult<Product> GetByProductId(int id)
        {
            var product = _productService.TGetByID(id);
            if(product != null)
            {
                return Ok(product);
            }
            return NotFound(ErrorMessages<Product>.NoItemFound + " with id= " + id);
        }

        [HttpPost("addnewproduct")]
        public ActionResult<GeneralProductResponseDto> AddNewProduct(CreateProductDto createProductDto)
        {
            Product productToAdd = new Product()
            {
                Description= createProductDto.Description,
                ImageUrl = createProductDto.ImageUrl,
                Price = createProductDto.Price,
                CategoryID = createProductDto.CategoryID,
                ProductName = createProductDto.ProductName,
                ProductStatus = createProductDto.ProductStatus
            };
            _productService.TAdd(productToAdd);
            GeneralProductResponseDto generalProductResponseDto = new GeneralProductResponseDto()
            {
                Item = productToAdd,
                Message = SucccessMessages<Product>.ItemAdded
            };
            return Ok(generalProductResponseDto);
        }

        [HttpPut("update")]
        public ActionResult<GeneralProductResponseDto> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var productToUpdate = _productService.TGetByID(updateProductDto.ProductID); 
            if(productToUpdate != null)
            {
                productToUpdate.Price = updateProductDto.Price;
                productToUpdate.ProductName = updateProductDto.ProductName;
                productToUpdate.ProductStatus = updateProductDto.ProductStatus;
                productToUpdate.CategoryID = updateProductDto.CategoryID;
                productToUpdate.Description = updateProductDto.Description;
                productToUpdate.ImageUrl = updateProductDto.ImageUrl;
                _productService.TUpdate(productToUpdate);
                GeneralProductResponseDto generalProductResponseDto = new GeneralProductResponseDto()
                {
                    Item = productToUpdate,
                    Message = SucccessMessages<Product>.ItemUpdated
                };
                return Ok(generalProductResponseDto);
            }
            return NotFound(ErrorMessages<Product>.NoItemFound + " with id= " + updateProductDto.ProductID);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult<GeneralProductResponseDto> DeleteProduct(int id)
        {
            var productToDelete = _productService.TGetByID(id);
            if(productToDelete != null ) { 
                _productService.TDelete(productToDelete);
                GeneralProductResponseDto generalProductResponseDto = new GeneralProductResponseDto()
                {
                    Item = productToDelete,
                    Message = SucccessMessages<Product>.ItemDeleted
                };
                return Ok(generalProductResponseDto);
            }
            return NotFound(ErrorMessages<Product>.NoItemFound + " with id= " + id);
        }
    }
}
