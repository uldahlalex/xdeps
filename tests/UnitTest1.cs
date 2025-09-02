using serversidevalidation;

namespace tests;

public class UnitTest1 : Setup
{
    
    [Theory]
    [InlineData("asd")]
    [InlineData("bads")]
    [InlineData("llkdsa")]
    public async Task Test1(string name)
    {
        var result = await GetService<IPetService>().CreatePet(new CreatePetRequestDto()
        {
            Age = 1,
            Name = name
        });
        if (result.Id.Length < 10)
            throw new Exception("Not a valid GUID");
        var allPets =   GetService<IPetService>().GetAllPets();
        if (allPets.Count != 1)
            throw new Exception("Expected exactly 1");
    }
    
    [Theory]
    [InlineData("asd")]
    [InlineData("bads")]
    [InlineData("llkdsa")]
    public async Task Test2(string name)
    {
        var result = await GetService<IPetService>().CreatePet(new CreatePetRequestDto()
        {
            Age = 1,
            Name = "Bob"
        });
        if (result.Id.Length < 10)
            throw new Exception("Not a valid GUID");
        var allPets =   GetService<IPetService>().GetAllPets();
        if (allPets.Count != 1)
            throw new Exception("Expected exactly 1");
    }
}
