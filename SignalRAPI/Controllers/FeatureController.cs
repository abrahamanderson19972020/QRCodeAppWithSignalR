using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.FeatureDto;
using EntitiesLayer.DTOs.ProductDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet("getallfeature")]
        public ActionResult<List<Feature>> GetAllFeatures()
        {
            var features = _featureService.TGetListAll();
            if (features != null)
            {
                return Ok(features);
            }
            return NotFound(ErrorMessages<Feature>.NoItemFound);
        }


        [HttpGet("getfeaturebyid/{id}")]
        public ActionResult<Feature> GetByFeatureId(int id)
        {
            var feature = _featureService.TGetByID(id);
            if (feature != null)
            {
                return Ok(feature);
            }
            return NotFound(ErrorMessages<Feature>.NoItemFound + " with id= " + id);
        }

        [HttpPost("addnewfeature")]
        public ActionResult AddNewFeature(CreateFeatureDto createFeatureDto)
        {
            Feature featureToAdd = new Feature()
            {
                Description1 = createFeatureDto.Description1,
                Description2 = createFeatureDto.Description2,
                Description3 = createFeatureDto.Description3,
                Title1 = createFeatureDto.Title1,
                Title2 = createFeatureDto.Title2,
                Title3 = createFeatureDto.Title3
            };
            _featureService.TAdd(featureToAdd);
            return Ok(SucccessMessages<Product>.ItemAdded);
        }

        [HttpPut("update")]
        public ActionResult UpdateProduct(UpdateFeatureDto updateFeatureDto)
        {
            var featureToUpdate = _featureService.TGetByID(updateFeatureDto.FeatureID);
            if (featureToUpdate != null)
            {
                featureToUpdate.Title1 = updateFeatureDto.Title1;
                featureToUpdate.Title2 = updateFeatureDto.Title2;
                featureToUpdate.Title3 = updateFeatureDto.Title3;
                featureToUpdate.Description1 = updateFeatureDto.Description1;
                featureToUpdate.Description2 = updateFeatureDto.Description2;
                featureToUpdate.Description3 = updateFeatureDto.Description3;
                _featureService.TUpdate(featureToUpdate);
                return Ok(SucccessMessages<Feature>.ItemUpdated);
            }
            return NotFound(ErrorMessages<Feature>.NoItemFound + " with id= " + updateFeatureDto.FeatureID);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var featureToDelete = _featureService.TGetByID(id);
            if (featureToDelete != null)
            {
                _featureService.TDelete(featureToDelete);
                return Ok(SucccessMessages<Feature>.ItemDeleted);
            }
            return NotFound(ErrorMessages<Feature>.NoItemFound + " with id= " + id);
        }
    }
}
