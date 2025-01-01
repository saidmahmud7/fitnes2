using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Workout
{
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; }
    [Required,MaxLength(500)]
    public string Description { get; set; }
    [Range(1,Int32.MaxValue)]
    public int  Duration { get; set; }
    [Range(1,Int32.MaxValue)]
    public int MaxParticipants { get; set; }
    public string Difficulty { get; set; }
    public bool IsActive { get; set; }
}