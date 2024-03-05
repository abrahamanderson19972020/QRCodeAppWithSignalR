using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.SocialMediaDto;
using EntitiesLayer.DTOs.TestimonialDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        [HttpGet("getalltestimonials")]
        public ActionResult<List<Testimonial>> GetAllTestimonials()
        {
            var testimonials = _testimonialService.TGetListAll();
            if(testimonials != null)
            {
                return Ok(testimonials);
            }
            return NotFound(ErrorMessages<Testimonial>.NoItemFound);
        }

        [HttpGet("testimonialbyid/{id}")]
        public ActionResult<Testimonial> GetTestimonialById(int id)
        {
            var testimonial = _testimonialService.TGetByID(id);
            if(testimonial != null)
            {
                return Ok(testimonial);
            }
            return NotFound(ErrorMessages<Testimonial>.NoItemFound + " with id = " + id);
        }

        [HttpPost("addnewtestimonial")]
        public ActionResult AddNewTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            Testimonial testimonialToCreate = new Testimonial()
            {
                Comment = createTestimonialDto.Comment,
                ImageUrl = createTestimonialDto.ImageUrl,
                Name = createTestimonialDto.Name,
                Status = createTestimonialDto.Status,
                Title = createTestimonialDto.Title
            };
            _testimonialService.TAdd(testimonialToCreate);
            return Ok(SucccessMessages<Testimonial>.ItemAdded);
        }

        [HttpPut("update")]
        public ActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            var testimonialToUpdate = _testimonialService.TGetByID(updateTestimonialDto.TestimonialID);
            if(testimonialToUpdate != null )
            {
                testimonialToUpdate.Title = updateTestimonialDto.Title;
                testimonialToUpdate.Comment = updateTestimonialDto.Comment;
                testimonialToUpdate.ImageUrl = updateTestimonialDto.ImageUrl;
                testimonialToUpdate.Name = updateTestimonialDto.Name;
                testimonialToUpdate.Status = updateTestimonialDto.Status;
                _testimonialService.TUpdate(testimonialToUpdate);
                return Ok(SucccessMessages<Testimonial>.ItemUpdated);
            }
            return NotFound(ErrorMessages<Testimonial>.NoItemFound + " with id = " + updateTestimonialDto.TestimonialID);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var testimonialToDelete = _testimonialService.TGetByID(id);
            if (testimonialToDelete != null)
            {
                _testimonialService.TDelete(testimonialToDelete);
                return Ok(SucccessMessages<Testimonial>.ItemDeleted);
            }
            return NotFound(ErrorMessages<Testimonial>.NoItemFound + " with id = " + id);
        }
    }
}
