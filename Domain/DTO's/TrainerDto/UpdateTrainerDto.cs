namespace Domain.DTO_s.TrainerDto;

public class UpdateTrainerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int Experience { get; set; }
    public string Status { get; set; }
    public string Specialization { get; set; }
}