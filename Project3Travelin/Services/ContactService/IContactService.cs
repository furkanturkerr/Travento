using Project3Travelin.Dtos.ContactForm;

namespace Project3Travelin.Services.ContactService;

public interface IContactService
{
    Task<List<ResultContactDto>> GetAllContactsAsync();
    Task UpdateContactAsync(UpdateContactDto updateContactDto);
    Task CreateContactAsync(CreateContactDto createContactDto);
    Task DeleteContactAsync(string id);
    Task<GetContactByIdDto> GetContactByIdAsync(string id);
}