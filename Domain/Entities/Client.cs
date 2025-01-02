using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Client
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string FirstName { get; set; }
    [Required, MaxLength(50)] 
    public string LastName { get; set; }
    [Phone]
    public string PhoneNumber { get; set; }
    [EmailAddress] 
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MembershipStatus { get; set; }
    
    public List<WorkoutSession> WorkoutSessions { get; set; }
}