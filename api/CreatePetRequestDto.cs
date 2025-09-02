using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace serversidevalidation;

public record CreatePetRequestDto
{
    [MinLength(3)][Required] public string Name { get; set; } = null!;
    [Range(0,15)][Required]
    public int Age { get; set; }
}