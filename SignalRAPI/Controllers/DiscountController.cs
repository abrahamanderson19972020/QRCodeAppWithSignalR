using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.DiscountDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpGet("getalldiscounts")]
        public ActionResult<List<Discount>> GetAllDiscounts()
        {
            var discounts = _discountService.TGetListAll();
            if(discounts != null)
            {
                return Ok(discounts);
            }
            return NotFound(ErrorMessages<Discount>.NoItemFound);
        }
        [HttpGet("getdiscountbyid/{id}")]
        public ActionResult<Discount> GetDiscountById(int id)
        {
            var discount = _discountService.TGetByID(id);
            if(discount != null)
            {
                return Ok(discount);
            }
            return NotFound(ErrorMessages<Discount>.NoItemFound + " with id= " + id);
        }
        [HttpPost("addnewdiscount")]
        public ActionResult AddNewDiscount(CreateDiscountDto createDiscountDto)
        {
            Discount discountToAdd = new Discount()
            {
                Status = createDiscountDto.Status,
                Amount = createDiscountDto.Amount,
                Description = createDiscountDto.Description,
                ImageUrl  = createDiscountDto.ImageUrl,
                Title = createDiscountDto.Title,
            };
            _discountService.TAdd(discountToAdd);
            return Ok(SucccessMessages<Contact>.ItemAdded);
        }
        [HttpPut("update")]
        public ActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            var discountToUpdate = _discountService.TGetByID(updateDiscountDto.DiscountID);
            if(discountToUpdate != null)
            {
               discountToUpdate.Status = updateDiscountDto.Status;
               discountToUpdate.ImageUrl = updateDiscountDto.ImageUrl;
               discountToUpdate.Description = updateDiscountDto.Description;
               discountToUpdate.Amount = updateDiscountDto.Amount;
               discountToUpdate.Title = updateDiscountDto.Title;
                _discountService.TUpdate(discountToUpdate);
                return Ok(SucccessMessages<Discount>.ItemAdded);
            }
            return NotFound(ErrorMessages<Discount>.NoItemFound + " with id= " + updateDiscountDto.DiscountID);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteDiscount(int id)
        {
            var discountToDelete = _discountService.TGetByID(id);
            if(discountToDelete != null)
            {
                _discountService.TDelete(discountToDelete);
                return Ok(SucccessMessages<Discount>.ItemDeleted);
            }
            return NotFound(ErrorMessages<Discount>.NoItemFound + " with id= " + id);
        }
    }
}
