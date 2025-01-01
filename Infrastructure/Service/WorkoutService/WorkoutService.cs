using System.Net;
using Domain.DTO_s.TrainerDto;
using Domain.DTO_s.WorkoutDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.WorkoutService;

public class WorkoutService(DataContext context) : IWorkoutService
{
    public async Task<ApiResponse<List<GetWorkoutDto>>> GetAll()
    {
        var workout =  context.Workouts.AsQueryable();

        var workoutDto = workout.Select(w => new GetWorkoutDto()
        {
            Name = w.Name,  
            Description = w.Description,
            Duration = w.Duration,
            MaxParticipants = w.MaxParticipants,
            Difficulty = w.Difficulty,
            IsActive = w.IsActive
        }).ToList();
        return new Task<ApiResponse<List<GetWorkoutDto>>>(workoutDto);


    }

    public async Task<ApiResponse<Workout>> GetById(int id)
    {
        var workout = await context.Workouts.FirstOrDefaultAsync(w => w.Id == id);
        return workout == null
            ? new ApiResponse<Workout>(HttpStatusCode.InternalServerError, "workout not Created")
            : new ApiResponse<Workout>( workout);
    }

    public async Task<ApiResponse<string>> Create(CreateWorkoutDto workout)
    {
        var workoutDto = new Workout()
        {
            Name = workout.Name,
            Description = workout.Description,
            Duration = workout.Duration,
            MaxParticipants = workout.MaxParticipants,
            Difficulty = workout.Difficulty,
            IsActive = true,
        };
        context.Workouts.AddAsync(workoutDto);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Trainer not Created")
            : new ApiResponse<string>("Created Succesfuly");
    }

    public async Task<ApiResponse<string>> Update(UpdateTrainerDto workout)
    {
        var existingWorkout = await context.Workouts.FirstOrDefaultAsync(w => w.Id == workout.Id);
    }

    public Task<ApiResponse<string>> Delete(int id)
    {
        throw new NotImplementedException();
    }
}