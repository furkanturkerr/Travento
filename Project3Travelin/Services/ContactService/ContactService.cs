using AutoMapper;
using MongoDB.Driver;
using Project3Travelin.Dtos.ContactForm;
using Project3Travelin.Entities;
using Project3Travelin.Settings;

namespace Project3Travelin.Services.ContactService;

public class ContactService : IContactService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Contact> _ContactsCollection;

    public ContactService(IMapper mapper, IDatabaseSettings _databaseSettings)
    {
        var client = new MongoClient(_databaseSettings.ConnectionString);
        var database = client.GetDatabase(_databaseSettings.DatabaseName);
        _ContactsCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName);
        _mapper = mapper;
    }

    public async Task<List<ResultContactDto>> GetAllContactsAsync()
    {
        var values = await _ContactsCollection.Find(x=> true).ToListAsync();
        return _mapper.Map<List<ResultContactDto>>(values);
    }

    public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
    {
        var value = _mapper.Map<Contact>(updateContactDto);
        await _ContactsCollection.FindOneAndReplaceAsync(x => x.ContactId == updateContactDto.ContactId, value);
    }

    public async Task CreateContactAsync(CreateContactDto createContactDto)
    {
        var values = _mapper.Map<Contact>(createContactDto);
        await _ContactsCollection.InsertOneAsync(values);
    }

    public async Task DeleteContactAsync(string id)
    {
        await _ContactsCollection.DeleteOneAsync(x => x.ContactId == id);
    }

    public async Task<GetContactByIdDto> GetContactByIdAsync(string id)
    {
        var vaalue = await _ContactsCollection.Find(x => x.ContactId == id).FirstOrDefaultAsync();
        return _mapper.Map<GetContactByIdDto>(vaalue);
    }
}