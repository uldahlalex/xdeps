using serversidevalidation;

namespace xdeps.Tests;

public class UnitTest1(IPetService petService)
{
    [Theory]
    [InlineData("asd")]
    [InlineData("bads")]
    [InlineData("llkdsa")]
    public async Task Test1(string name)
    {
        var result = await petService.CreatePet(new CreatePetRequestDto()
        {
            Age = 1,
            Name = name
        });
        if (result.Id.Length < 10)
            throw new Exception("Not a valid GUID");
        var allPets = petService.GetAllPets();
        if (allPets.Count != 1)
            throw new Exception("Expected exactly 1");
    }
    
    [Theory]
    [InlineData("asd")]
    [InlineData("bads")]
    [InlineData("llkdsa")]
    public async Task Test2(string name)
    {
        var result = await petService.CreatePet(new CreatePetRequestDto()
        {
            Age = 1,
            Name = "Bob"
        });
        if (result.Id.Length < 10)
            throw new Exception("Not a valid GUID");
        var allPets = petService.GetAllPets();
        if (allPets.Count != 1)
            throw new Exception("Expected exactly 1");
    }
}
