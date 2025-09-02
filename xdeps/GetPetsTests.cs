using Infrastructure.Postgres.Scaffolding;
using serversidevalidation;
using serversidevalidation.Entities;

namespace xdeps;

public class GetPetsTests(IPetService petService, MyDbContext ctx)
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
        var result = await petService.GetAllPets();
        Assert.Equivalent(existing, result.First());
    }
}