using AutoMapper;
using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.CategoryDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet("getallcategories")]
        public ActionResult<List<Category>> GetAllCategories()
        {
            var categories = _categoryService.TGetListAll();
            if(categories != null)
            {
                var mappedCategories = _mapper.Map<List<ResultCategoryDto>>(categories);
                return Ok(categories);
            }
            return NotFound(ErrorMessages<Category>.NoItemFound);  
        }
        [HttpGet("getcategorybyid/{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _categoryService.TGetByID(id);
            if(category != null)
            {
                return Ok(category);
            }
            return NotFound(ErrorMessages<Category>.NoItemFound + "with id= " + id);
        }
        [HttpPost("addnewcategory")]
        public ActionResult<CategoryGeneralResponseDto> AddNewCategory(CreateCategoryDto createCategoryDto)
        {
            Category newCategory = new Category()
            {
                CategoryName = createCategoryDto.CategoryName,
                Status = createCategoryDto.Status,
            };
            _categoryService.TAdd(newCategory);
            CategoryGeneralResponseDto responseDto = new CategoryGeneralResponseDto()
            {
                Item = newCategory,
                Message = SucccessMessages<Category>.ItemAdded
            };
            return Ok(responseDto);
        }
        [HttpPut("update")]
        public ActionResult<CategoryGeneralResponseDto> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var categoryToUpdate = _categoryService.TGetByID(updateCategoryDto.CategoryID);
            if(categoryToUpdate != null)
            {
                categoryToUpdate.Status = updateCategoryDto.Status;
                categoryToUpdate.CategoryName = updateCategoryDto.CategoryName;
                _categoryService.TUpdate(categoryToUpdate);
                CategoryGeneralResponseDto responseDto = new CategoryGeneralResponseDto()
                {
                    Item = new Category()
                    {
                        CategoryID = categoryToUpdate.CategoryID,
                        Status = updateCategoryDto.Status,
                        CategoryName = categoryToUpdate.CategoryName,
                        Products = categoryToUpdate.Products
                    },
                    Message = SucccessMessages<Category>.ItemUpdated
                };
                return Ok(responseDto);
            }
            return NotFound(ErrorMessages<Category>.NoItemFound + " with id= " + updateCategoryDto.CategoryID);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var categoryToDelete = _categoryService.TGetByID(id);
            if(categoryToDelete != null )
            {
                _categoryService.TDelete(categoryToDelete);
                CategoryGeneralResponseDto responseDto = new CategoryGeneralResponseDto()
                {
                    Item = categoryToDelete,
                    Message = SucccessMessages<Category>.ItemDeleted
                };
                return Ok(responseDto);
            }
            return NotFound(ErrorMessages<Category>.NoItemFound + " with id= " + id);
        }
    }
}
