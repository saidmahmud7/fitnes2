namespace Domain.DTO_s.WorkoutDto;

public class UpdateWorkoutDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int  Duration { get; set; }
    public int MaxParticipants { get; set; }
    public string Difficulty { get; set; }
    public bool IsActive { get; set; }
}