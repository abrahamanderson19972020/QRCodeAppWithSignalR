using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.AboutDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        [HttpGet("getallabouts")]
        public ActionResult<List<About>> GetAllAbouts()
        {
            var abouts = _aboutService.TGetListAll();
            if(abouts == null) return NotFound(ErrorMessages<About>.NoItemFound);
            return Ok(abouts);
        }
        [HttpGet("{id}")]
        public ActionResult<About> GetSingleAbout(int id)
        {
            var about = _aboutService.TGetByID(id);
            if (about == null)
            {
                return NotFound(ErrorMessages<About>.NoItemFound + " with id= "+ id);
            } 
            return Ok(about);
        }
        [HttpPost("addnewabout")]
        public ActionResult AddNewAbout(CreateAboutDto about)
        {
            About newAbout = new About()
            {
                Title = about.Title,
                Description = about.Description,
                ImageUrl = about.ImageUrl
            };   

           _aboutService.TAdd(newAbout);
            return Ok(SucccessMessages<About>.ItemAdded);
        }
        [HttpPut("update")]
        public ActionResult UpdateAbout(UpdateAboutDto about)
        {
            var aboutToUpdate = _aboutService.TGetByID(about.AboutID);
            if (aboutToUpdate != null)
            {
                aboutToUpdate.Title = about.Title;
                aboutToUpdate.Description = about.Description;
                aboutToUpdate.ImageUrl = about.ImageUrl;
                _aboutService.TUpdate(aboutToUpdate);
                return Ok(SucccessMessages<About>.ItemUpdated);
            }
            return NotFound(ErrorMessages<About>.NoItemFound);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteAbout(int id)
        {
            var about = _aboutService.TGetByID(id);
            if(about != null)
            {
                _aboutService.TDelete(about);
                return Ok(SucccessMessages<About>.ItemDeleted);
            }
            return NotFound(ErrorMessages<About>.NoItemFound + " with id= " + id);
        }
    }
}
