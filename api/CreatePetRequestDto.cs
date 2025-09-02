using System.ComponentModel.DataAnnotations;

namespace serversidevalidation;

public record CreatePetRequestDto
{
    [MinLength(3)] public string Name { get; set; } = null!;
    [Range(0,15)]
    public int Age { get; set; }
}