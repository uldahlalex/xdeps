using System.ComponentModel.DataAnnotations;
using Infrastructure.Postgres.Scaffolding;
using serversidevalidation;
using serversidevalidation.Entities;

namespace xdeps;

public class UpdatePetTests(IPetService petService, MyDbContext ctx)
{
    
    [Fact]
    public async Task UpdatePet_ShouldSuccessfullyUpdate()
    {
        var id = Guid.NewGuid().ToString();
        var initialName = "Bob";
        var timestamp = DateTime.UtcNow;
        var initialAge = 2;
        var existing = new Pet(id: id, name: initialName, createdAt: timestamp, age: 2);
        await ctx.Pets.AddAsync(existing);
        await ctx.SaveChangesAsync();

        var newAge = 5;
        var newName = "Bobby";
        var validUpdate = new UpdatePetRequestDto(newName, newAge, id);
        var result = await petService.UpdatePet(validUpdate);
        var objectInDb = ctx.Pets.First(); 
        Assert.Equal(newAge, result.Age);
        Assert.Equal(newAge, objectInDb.Age);
        Assert.Equal(newName, result.Name);
        Assert.Equal(newName, objectInDb.Name);
    }
    
    
    [Theory]
    [InlineData("", 7)]
    [InlineData(null, 7)]
    [InlineData("a", 7)]
    [InlineData("aa", 7)]
    [InlineData("aaa", -1)]
    [InlineData("aaa", 16)]
    public async Task UpdatePet_ShouldGetValidationException_IfDataAnnotationsGetViolated(string name, int age)
    {
        await Assert.ThrowsAsync<ValidationException>(async () =>
            await petService.UpdatePet(
                new UpdatePetRequestDto(name, age, "" //It should be redundant since it should throw validation exc before DB command
            )));

    }

    [Fact]
    public async Task UpdatePet_ShouldThrowExceptionIfIdDoesNotExist()
    {
        await Assert.ThrowsAnyAsync<Exception>(async () => await petService.UpdatePet(new UpdatePetRequestDto("Bob", 2, Guid.NewGuid().ToString())));
    }
}