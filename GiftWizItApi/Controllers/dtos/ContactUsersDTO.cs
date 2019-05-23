using GiftWizItApi.Models;

namespace GiftWizItApi.Controllers.dtos
{
    public class ContactUsersDTO
    {
        public int ContactId { get; set; }
        public Contacts Contact { get; set; }

        public string UserId { get; set; }
        public Users User { get; set; }
    }
}
