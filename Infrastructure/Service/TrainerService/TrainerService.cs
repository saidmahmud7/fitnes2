using System.Net;
using Domain.DTO_s;
using Domain.DTO_s.TrainerDto;
using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Data;
using Infrastructure.Response;
using Infrastructure.Service.TrainerService;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TrainerService(DataContext context) : ITrainerService
{
    public async Task<ApiResponse<List<GetTrainerDto>>> GetAll()
    {
        var c = context.Trainers.Include(x => x.WorkoutSessions).AsQueryable();

        var trainers = c.Select(f => new GetTrainerDto()
        {
            FirstName = f.FirstName,
            LastName = f.LastName,
            PhoneNumber = f.PhoneNumber,
            Experience = f.Experience,
            Status = f.Status,
            Specialization = f.Specialization,
            WorkoutSessions = f.WorkoutSessions.Select(w => new GetWorkoutSessionDto()
            {
                SessionDate = w.SessionDate,
                StartTime = w.StartTime,
                EndTime = w.EndTime,
                Status = w.Status,
                Comment = w.Comment,
                MaxCapacity = w.MaxCapacity,
                CurrentParticipants = w.CurrentParticipants,
                CreatedAt = w.CreatedAt,
            }).ToList()
        }).ToList();
        return new ApiResponse<List<GetTrainerDto>>(trainers);
    }

    public async Task<ApiResponse<Trainer>> GetById(int id)
    {
        var trainer = await context.Trainers.FirstOrDefaultAsync(w => w.Id == id);
        return trainer == null
            ? new ApiResponse<Trainer>(HttpStatusCode.InternalServerError, "trainer not Created")
            : new ApiResponse<Trainer>(trainer);
    }

    public async Task<ApiResponse<string>> Create(CreateTrainerDto trainerDTO)
    {
        var trainer = new Trainer()
        {
            FirstName = trainerDTO.FirstName,
            LastName = trainerDTO.LastName,
            PhoneNumber = trainerDTO.PhoneNumber,
            Experience = trainerDTO.Experience,
            Status = trainerDTO.Status,
            Specialization = trainerDTO.Specialization
        };
        context.Trainers.Add(trainer);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.Created, "Trainer Added");
    }

    public async Task<ApiResponse<string>> Update(UpdateTrainerDto trainerDTO)
    {
        var existingTrainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == trainerDTO.Id);
        if (existingTrainer == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Trainer not found");
        }

        existingTrainer.FirstName = trainerDTO.FirstName;
        existingTrainer.LastName = trainerDTO.LastName;
        existingTrainer.PhoneNumber = trainerDTO.PhoneNumber;
        existingTrainer.Experience = trainerDTO.Experience;
        existingTrainer.Status = trainerDTO.Status;
        existingTrainer.Specialization = trainerDTO.Specialization;
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.Created, "Trainer updated");
    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        var existingTrainer = await context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
        if (existingTrainer == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Trainer not found");
        }

        context.Trainers.Remove(existingTrainer);
        var result = await context.SaveChangesAsync();
        return result == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.Created, "Trainer deleted");
    }
}