using System.ComponentModel.DataAnnotations;
using Infrastructure.Postgres.Scaffolding;
using serversidevalidation;

namespace xdeps.Tests;

public class CreatePetTests(IPetService petService, MyDbContext dbContext)
{
    [Fact]
    public async Task CreatePet_ShouldSuccessfullyCreatePet_FromDto()
    {
        var validDto = new CreatePetRequestDto()
        {
            Age = 5,
            Name = "Bob"
        };
        var result = await petService.CreatePet(validDto);
        //Check valid GUID
        Assert.Matches(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$", result.Id);
        //Check timestamp is within the last minute
        if (result.CreatedAt > DateTime.UtcNow)
            throw new Exception("Timestamp cannot be in the future");
        if (result.CreatedAt.AddMinutes(1) < DateTime.UtcNow)
            throw new Exception("Timestamp is not within 1 minute");
        Assert.Equal("Bob", result.Name);
        Assert.Equal(5, result.Age);
        Assert.Equivalent(dbContext.Pets.First(), result);
    }

    [Theory]
    [InlineData("", 7)]
    [InlineData(null, 7)]
    [InlineData("a", 7)]
    [InlineData("aa", 7)]
    [InlineData("aaa", -1)]
    [InlineData("aaa", 16)]
    public async Task CreatePet_ShouldGetValidationException_IfDataAnnotationsGetViolated(string name, int age)
    {
        var invalidDto = new CreatePetRequestDto()
        {
            Age = age,
            Name = name
        };
        await Assert.ThrowsAsync<ValidationException>(async () => await petService.CreatePet(invalidDto));

    }
}
