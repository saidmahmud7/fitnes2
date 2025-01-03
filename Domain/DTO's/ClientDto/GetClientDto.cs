﻿using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;

namespace Domain.DTO_s.ClientDto;

public class GetClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MembershipStatus { get; set; }
    public List<GetWorkoutSessionDto> WorkoutSessions { get; set; }
}