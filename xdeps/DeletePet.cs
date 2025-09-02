using Infrastructure.Postgres.Scaffolding;
using Microsoft.EntityFrameworkCore;
using serversidevalidation;
using serversidevalidation.Entities;

namespace xdeps;

public class DeletePet(IPetService petService, MyDbContext ctx)
{
    
    [Fact]
    public async Task DeletePet_ShouldRemoveAnExistingPet()
    {
        var id = Guid.NewGuid().ToString();
        var initialName = "Bob";
        var timestamp = DateTime.UtcNow;
        var initialAge = 2;
        var existing = new Pet(id: id, name: initialName, createdAt: timestamp, age: 2);
        await ctx.Pets.AddAsync(existing);
        await ctx.SaveChangesAsync();
       
        _ = await petService.DeletePet(id);
        var rows = await ctx.Pets.CountAsync();
        Assert.Equal(rows, 0);
    }
    
    [Fact]
    public async Task DeletePet_ShouldThrowExceptionIfIdDoesNotExist()
    {
        await Assert.ThrowsAnyAsync<Exception>(async () => await petService.DeletePet(Guid.NewGuid().ToString()));
    }
}