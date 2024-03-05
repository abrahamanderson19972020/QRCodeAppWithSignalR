using AutoMapper;
using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.ContactDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        [HttpGet("getallcontacts")]
        public ActionResult<List<Contact>> GetAllContacts()
        {
            var contacts = _contactService.TGetListAll();
            if(contacts != null)
            {
                return Ok(_mapper.Map<List<ResultContactDto>>(contacts));
            }
            return NotFound(ErrorMessages<Contact>.NoItemFound);
        }
        [HttpGet("getcontactbyid/{id}")]
        public ActionResult<Contact> GetContactById(int id) { 
            var contact = _contactService.TGetByID(id);
            if(contact != null)
            {
                return Ok(contact);
            }
            return NotFound(ErrorMessages<Contact>.NoItemFound + " with id= " + id);
        }
        [HttpPost("addnewcontact")]
        public ActionResult AddNewContact(CreateContactDto createContactDto)
        {
            Contact contactToCreate = new Contact()
            {
               FooterDescription = createContactDto.FooterDescription,
               FooterTitle = createContactDto.FooterTitle,
               Location = createContactDto.Location,
               Mail = createContactDto.Mail,
               Phone = createContactDto.Phone,
               OpenDays = createContactDto.OpenDays,
               OpenDaysDescription = createContactDto.OpenDaysDescription,
               OpenHours = createContactDto.OpenHours
            };
            _contactService.TAdd(contactToCreate);
            return Ok(SucccessMessages<Contact>.ItemAdded);
        }
        [HttpPut("update")]
        public ActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            var contactToUpdate = _contactService.TGetByID(updateContactDto.ContactID);
            if(contactToUpdate != null)
            {
                contactToUpdate.FooterTitle = updateContactDto.FooterDescription;
                contactToUpdate.FooterDescription = updateContactDto.FooterTitle;
                contactToUpdate.OpenDaysDescription = updateContactDto.OpenDaysDescription;
                contactToUpdate.OpenDays = updateContactDto.OpenDays;
                contactToUpdate.OpenHours = updateContactDto.OpenHours;
                contactToUpdate.Location = updateContactDto.Location;
                contactToUpdate.Phone = updateContactDto.Phone;
                contactToUpdate.Mail = updateContactDto.Mail;
                _contactService.TUpdate(contactToUpdate);
                return Ok(SucccessMessages<Contact>.ItemUpdated);
            }
            return NotFound(ErrorMessages<Contact>.NoItemFound + " with id= " + updateContactDto.ContactID);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteContact(int contactID) 
        { 
        var contactToDelete = _contactService.TGetByID(contactID);
        if(contactToDelete != null)
            {
                _contactService.TDelete(contactToDelete);
                return Ok(SucccessMessages<Contact>.ItemDeleted);
            }
            return NotFound(ErrorMessages<Contact>.NoItemFound + " with id= " + contactID);
        }      
    }
}
