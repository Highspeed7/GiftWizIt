using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GiftWizItApi.Controllers.dtos;
using GiftWizItApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizItApi.Controllers
{
    public class Test
    {
        public string valueToSend { get; set; }
    }

    [Authorize]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactEmailer emailer;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ContactsController(IContactEmailer emailer, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.emailer = emailer;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IUnitOfWork UnitOfWork { get; }

        [Route("api/Contacts/Get")]
        [HttpGet]
        public async Task<IEnumerable<ContactUsersDTO>> GetAllUserContacts()
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;
            var results = await unitOfWork.ContactUsers.GetAllUserContacts(userId);

            return mapper.Map<List<ContactUsersDTO>>(results);
        }

        [Route("api/Contacts/Add")]
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactDTO contact)
        {
            var userId = User.Claims.First(e => e.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier").Value;

            // Verify that the contact hasn't already been added
            var existingContact = await unitOfWork.Contacts.GetContactByEmail(contact.Email);

            if (existingContact != null)
            {
                // TODO: Utilize UI logic to prevent this from occurring to prevent more server calls.
                // Verify that the existing contact is not already a part of the users contacts
                var existingUserContact = await unitOfWork.ContactUsers.GetUserContactById(existingContact.ContactId, userId);
                if(existingUserContact != null)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Contact already exists for user");
                }

                unitOfWork.ContactUsers.Add(contact, userId, mapper.Map<ContactDTO>(existingContact));
            }else
            {
                unitOfWork.ContactUsers.Add(contact, userId);
            }

            await unitOfWork.CompleteAsync();

            return StatusCode((int)HttpStatusCode.OK);
        }

        private async Task<bool> SendEmail(Test valueToSend)
        {
            return true;
        }

        //[Route("api/Contacts/Email")]
        //[HttpPost]
        //public async Task<IActionResult> SendEmail(Test valueToSend)
        //{
        //    var to = "brwest@enlistedinnovations.com";

        //    ContactMailTemplate templateData = new ContactMailTemplate()
        //    {
        //        contactName = "Lauren",
        //        from = "Brian West",
        //        getStartedUrl = "https://www.giftwizit.com"
        //    };

        //    await emailer.SendEmailTransactionalAsync(to, templateData);
        //    return StatusCode((int)HttpStatusCode.OK);
        //}
    }
}