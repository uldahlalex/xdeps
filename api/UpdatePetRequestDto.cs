using System.ComponentModel.DataAnnotations;

namespace serversidevalidation;

public record UpdatePetRequestDto
{
    public UpdatePetRequestDto(string name, int age, string id)
    {
        Name = name;
        Age = age;
        Id = id;
    }

    [MinLength(2)]
    public string Name { get; set; }
    [Range(0,15)]
    public int Age { get; set; }
    /// <summary>
    /// ID is used for retrieving existing pet, not for updating the ID value
    /// </summary>
    public string Id { get; set; }
}