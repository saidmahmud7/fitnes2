using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class WorkoutSession
{
    public int Id { get; set; }
    public DateTime SessionDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public  string Status { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentParticipants { get; set; }
    [Required,MaxLength(200)]
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    //foreign Key
    public int ClientId { get; set; }
    public Client Client { get; set; }
    
    public  int WorkoutId { get; set; }
    public Workout Workout { get; set; }
    public  int TrainerId { get; set; }
    public Trainer Trainer { get; set; }
}
