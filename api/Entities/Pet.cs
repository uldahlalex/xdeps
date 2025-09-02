using System;
using System.Collections.Generic;

namespace serversidevalidation.Entities;

public partial class Pet
{
    public Pet(string id, string name, DateTime createdAt, int age, string description)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        Age = age;
        Description = description;
    }

    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int Age { get; set; }

    public string Description { get; set; } = null!;
}
