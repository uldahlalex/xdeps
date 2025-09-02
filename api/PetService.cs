using System.ComponentModel.DataAnnotations;

namespace serversidevalidation;

public interface IPetService
{
    Task<Pet> CreatePet(CreatePetRequestDto pet);
    Pet UpdatePet(UpdatePetRequestDto pet);
    Pet DeletePet(string petId);
    List<Pet> GetAllPets();
}

public class PetService(MyDbContext _db) : IPetService
{

    public async Task<Pet> CreatePet(CreatePetRequestDto pet)
    {
        Validator.ValidateObject(pet, 
            new ValidationContext(pet), 
            true);
        var petEntity = new Pet(
            age: pet.Age,
            name: pet.Name, 
            createdAt: DateTime.UtcNow,
            id: Guid.NewGuid().ToString(),
            description: "hardcoded description"); 
       await _db.Pets.AddAsync(petEntity);
       await _db.SaveChangesAsync();
      return petEntity;
    }

    public Pet UpdatePet(UpdatePetRequestDto pet)
    {
        Validator.ValidateObject(pet, 
            new ValidationContext(pet), 
            true);
        var existingPet = _db.Pets.First(p => p.Id == pet.Id);
        existingPet.Age = pet.Age;
        existingPet.Name = pet.Name;
        _db.SaveChanges();
        return existingPet;
    }

    public Pet DeletePet(string petId)
    {
        var existingPet = _db.Pets.First(p => p.Id == petId);
        _db.Pets.Remove(existingPet);
        _db.SaveChanges();
        return existingPet;
    }

    public List<Pet> GetAllPets()
    {
        return _db.Pets.ToList();
    }
}