namespace Domain.DTO_s.WorkoutSessionDto;

public class UpdatedSessionDto
{
    public int Id { get; set; }
    public DateTime SessionDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public  string? Status { get; set; }
    public int MaxCapacity { get; set; }
    public int CurrentParticipants { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ClientId { get; set; }
    public  int WorkoutId { get; set; }
    public  int TrainerId { get; set; }
}