using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Trainer
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string FirstName { get; set; }
    [Required,MaxLength(50)]
    public string LastName { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    [Range(1,Int32.MaxValue)]
    public int Experience { get; set; }
    public string Status { get; set; }
    [Required,MaxLength(100)]
    public string Specialization { get; set; }
    
    public List<WorkoutSession> WorkoutSessions { get; set; }
}