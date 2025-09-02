using Microsoft.AspNetCore.Mvc;
using serversidevalidation.Entities;

namespace serversidevalidation;

[ApiController]
public class PetController(IPetService petService) : ControllerBase
{
    
    [HttpPost(nameof(CreatePet))]
    public async Task<Pet> CreatePet([FromBody]CreatePetRequestDto p)
    {
        var result = await petService.CreatePet(p);
        return result;
    }

    [HttpPatch(nameof(UpdatePet))]
    public async Task<Pet> UpdatePet([FromBody]UpdatePetRequestDto p)
    {
        return await petService.UpdatePet(p);
    }

    [HttpDelete(nameof(DeletePet))]
    public async Task<Pet> DeletePet(string petId)
    {
        return await petService.DeletePet(petId);
    }

    [HttpGet(nameof(GetAllPets))]
    public async Task<List<Pet>> GetAllPets()
    {
        return await petService.GetAllPets();
    }
    
}