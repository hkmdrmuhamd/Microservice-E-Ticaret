using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Services.ContactServices;

namespace MultiShop.Catalog.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactList()
        {
            var values = await _contactService.GetContactListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactByIdContact(string id)
        {
            var value = await _contactService.GetContactByIdAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _contactService.CreateContactAsync(createContactDto);
            return Ok("İletişim bilgileri başarıyla eklendi.");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _contactService.UpdateContactAsync(updateContactDto);
            return Ok("İletişim bilgileri başarıyla güncellendi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await _contactService.DeleteContactAsync(id);
            return Ok("İletişim bilgileri başarıyla silindi.");
        }
    }
}
