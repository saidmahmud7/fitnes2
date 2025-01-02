using System.Net;
using Domain.DTO_s.TrainerDto;
using Domain.DTO_s.WorkoutDto;
using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.WorkoutService;

public class WorkoutService(DataContext context) : IWorkoutService
{
    public async Task<ApiResponse<List<GetWorkoutDto>>> GetAll()
    {
        var workout = context.Workouts
            .Include(x => x.WorkoutSessions)
            .AsQueryable();

        var workoutDto = workout.Select(w => new GetWorkoutDto()
        {
            Name = w.Name,
            Description = w.Description,
            Duration = w.Duration,
            MaxParticipants = w.MaxParticipants,
            Difficulty = w.Difficulty,
            IsActive = w.IsActive,
            WorkoutSessions = w.WorkoutSessions.Select(s => new GetWorkoutSessionDto()
            {
                SessionDate = s.SessionDate,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Status = s.Status,
                Comment = s.Comment,
                MaxCapacity = s.MaxCapacity,
                CurrentParticipants = s.CurrentParticipants,
                CreatedAt = s.CreatedAt,
            }).ToList()
        }).ToList();
        return new ApiResponse<List<GetWorkoutDto>>(workoutDto);
    }

    public async Task<ApiResponse<Workout>> GetById(int id)
    {
        var workout = await context.Workouts.FirstOrDefaultAsync(w => w.Id == id);
        return workout == null
            ? new ApiResponse<Workout>(HttpStatusCode.InternalServerError, "workout not Created")
            : new ApiResponse<Workout>(workout);
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

    public async Task<ApiResponse<string>> Update(UpdateWorkoutDto workout)
    {
        var existingWorkout = await context.Workouts.FirstOrDefaultAsync(w => w.Id == workout.Id);
        if (existingWorkout == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Workout not Found");
        }

        existingWorkout.Name = workout.Name;
        existingWorkout.Description = workout.Description;
        existingWorkout.Duration = workout.Duration;
        existingWorkout.MaxParticipants = workout.MaxParticipants;
        existingWorkout.Difficulty = workout.Difficulty;
        existingWorkout.IsActive = workout.IsActive;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.Created, "Workout updated");
    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        var existingWorkout = await context.Workouts.FirstOrDefaultAsync(w => w.Id == id);
        if (existingWorkout == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Workout not Found");
        }

        context.Workouts.Remove(existingWorkout);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.Created, "Workout deleted");
    }
}