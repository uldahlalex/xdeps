using Microsoft.AspNetCore.Mvc;

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
    public Pet UpdatePet([FromBody]UpdatePetRequestDto p)
    {
        return petService.UpdatePet(p);
    }

    [HttpDelete(nameof(DeletePet))]
    public Pet DeletePet(string petId)
    {
        return petService.DeletePet(petId);
    }

    [HttpGet(nameof(GetAllPets))]
    public List<Pet> GetAllPets()
    {
        return petService.GetAllPets();
    }
    
}