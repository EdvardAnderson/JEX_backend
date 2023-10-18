using System;

namespace JEX_backend.Models;
public class Company
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }

    public List<JobOpening>? JobOpenings {get;set;}

    public Company()
    {
        JobOpenings = new List<JobOpening>();
    }
}