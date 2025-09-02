using System.ComponentModel.DataAnnotations;
using Infrastructure.Postgres.Scaffolding;
using serversidevalidation.Entities;

namespace serversidevalidation;

public interface IPetService
{
    Task<Pet> CreatePet(CreatePetRequestDto pet);
    Task<Pet> UpdatePet(UpdatePetRequestDto pet);
    Task<Pet> DeletePet(string petId);
    Task<List<Pet>> GetAllPets();
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
            id: Guid.NewGuid().ToString()); 
       await _db.Pets.AddAsync(petEntity);
       await _db.SaveChangesAsync();
      return petEntity;
    }

    public async Task<Pet> UpdatePet(UpdatePetRequestDto pet)
    {
        Validator.ValidateObject(pet, 
            new ValidationContext(pet), 
            true);
        var existingPet = _db.Pets.First(p => p.Id == pet.Id);
        existingPet.Age = pet.Age;
        existingPet.Name = pet.Name;
        await _db.SaveChangesAsync();
        return existingPet;
    }

    public async Task<Pet> DeletePet(string petId)
    {
        var existingPet = _db.Pets.First(p => p.Id == petId);
        _db.Pets.Remove(existingPet);
        await _db.SaveChangesAsync();
        return existingPet;
    }

    public async Task<List<Pet>> GetAllPets()
    {
        return _db.Pets.ToList();
    }
}