using System.Net;
using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.WorkoutSessionService;

public class WorkoutSessionService(DataContext context) : IWorkoutSessionService
{
    public async Task<ApiResponse<List<GetWorkoutSessionDto>>> GetAll()
    {
        var session = context.WorkoutSessions
            .AsQueryable();

        var sessionDto = context.WorkoutSessions.Select(s => new GetWorkoutSessionDto()
        {
            SessionDate = s.SessionDate,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Status = s.Status,
            MaxCapacity = s.MaxCapacity,
            CurrentParticipants = s.CurrentParticipants,
            Comment = s.Comment,
        }).ToList();
        return new ApiResponse<List<GetWorkoutSessionDto>>(sessionDto);
    }

    public async Task<ApiResponse<WorkoutSession>> GetById(int id)
    {
        var session = await context.WorkoutSessions.FirstOrDefaultAsync(w => w.Id == id);
        if (session == null)
            return new ApiResponse<WorkoutSession>(HttpStatusCode.NotFound, "Session not Found");
        return new ApiResponse<WorkoutSession>(session);
    }

    public async Task<ApiResponse<string>> Create(CreateWorkoutSessionDto s)
    {
        var sessions = new WorkoutSession()
        {
            SessionDate = s.SessionDate,
            StartTime = s.StartTime,
            EndTime = s.EndTime,
            Status = s.Status,
            MaxCapacity = s.MaxCapacity,
            CurrentParticipants = s.CurrentParticipants,
            Comment = s.Comment,
            TrainerId = s.TrainerId,
            ClientId = s.ClientId,
            WorkoutId = s.WorkoutId
        };
        await context.WorkoutSessions.AddAsync(sessions);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "InternalServerError")
            : new ApiResponse<string>(HttpStatusCode.Created, "Add Succesfull");
    }

    public async Task<ApiResponse<string>> Updated(UpdatedSessionDto session)
    {
        var existingSession = await context.WorkoutSessions.FirstOrDefaultAsync(w => w.Id == session.Id);
        if (existingSession == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Not Found");
        }

        existingSession.SessionDate = session.SessionDate;
        existingSession.StartTime = session.StartTime;
        existingSession.EndTime = session.EndTime;
        existingSession.Status = session.Status;
        existingSession.MaxCapacity = session.MaxCapacity;
        existingSession.CurrentParticipants = session.CurrentParticipants;
        existingSession.Comment = session.Comment;
        existingSession.CreatedAt = session.CreatedAt;
        existingSession.TrainerId = session.TrainerId;
        existingSession.ClientId = session.ClientId;
        existingSession.WorkoutId = session.WorkoutId;
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "InternalServerError")
            : new ApiResponse<string>(HttpStatusCode.Created, "Update Succesfull");
    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        
        var existingSession = await context.WorkoutSessions.FirstOrDefaultAsync(w => w.Id == id);
        if (existingSession == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Not Found");
        }

        context.WorkoutSessions.Remove(existingSession);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "InternalServerError")
            : new ApiResponse<string>(HttpStatusCode.Created, "Deleted Succesfull");
    }
}