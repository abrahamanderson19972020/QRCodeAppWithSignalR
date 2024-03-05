using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.FeatureDto;
using EntitiesLayer.DTOs.SocialMediaDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialMediaService;
        public SocialMediaController(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        [HttpGet("getallsocialmedia")]
        public ActionResult<List<SocialMedia>> GetAllSocialMedias()
        {
            var socialMedias = _socialMediaService.TGetListAll();
            if(socialMedias != null)
            {
                return Ok(socialMedias);
            }
            return NotFound(ErrorMessages<SocialMedia>.NoItemFound);
        }

        [HttpGet("getsocialmediabyid/{id}")]
        public ActionResult<SocialMedia> GetSocialMediaById(int id) { 
         var socialMedia = _socialMediaService.TGetByID(id);
            if(socialMedia != null)
            {
                return Ok(socialMedia);
            }
        return NotFound(ErrorMessages<SocialMedia>.NoItemFound + " with id = " + id);
        }

        [HttpPost("addnewsocialmedia")]
        public ActionResult AddNewSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            SocialMedia socialMediaToAdd = new SocialMedia()
            {
                Title = createSocialMediaDto.Title,
                Icon = createSocialMediaDto.Icon,
                Url = createSocialMediaDto.Url
            };
            _socialMediaService.TAdd(socialMediaToAdd);
            return Ok(SucccessMessages<SocialMedia>.ItemAdded);
        }

        [HttpPut("update")]
        public ActionResult UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var socialMediaToUpdate = _socialMediaService.TGetByID(updateSocialMediaDto.SocialMediaID);
            if (socialMediaToUpdate != null)
            {
                socialMediaToUpdate.Title = updateSocialMediaDto.Title;
                socialMediaToUpdate.Url = updateSocialMediaDto.Url;
                socialMediaToUpdate.Icon = updateSocialMediaDto.Icon;
                _socialMediaService.TUpdate(socialMediaToUpdate);
                return Ok(SucccessMessages<SocialMedia>.ItemUpdated);
            }
            return NotFound(ErrorMessages<SocialMedia>.NoItemFound + " with id = " + updateSocialMediaDto.SocialMediaID);
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var socialMediaToDelete = _socialMediaService.TGetByID(id);
            if (socialMediaToDelete != null)
            {
                _socialMediaService.TDelete(socialMediaToDelete);
                return Ok(SucccessMessages<SocialMedia>.ItemDeleted);
            }
            return NotFound(ErrorMessages<SocialMedia>.NoItemFound + " with id = " + id);
        }
    }
}
