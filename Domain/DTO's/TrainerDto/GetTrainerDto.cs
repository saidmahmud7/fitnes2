using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;

namespace Domain.DTO_s.TrainerDto;

public class GetTrainerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int Experience { get; set; }
    public string Status { get; set; }
    public string Specialization { get; set; }
    public List<GetWorkoutSessionDto> WorkoutSessions { get; set; }

}