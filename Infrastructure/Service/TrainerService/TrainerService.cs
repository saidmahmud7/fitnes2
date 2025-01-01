using System.Net;
using Domain.DTO_s.TrainerDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.TrainerService;

public class TrainerService(DataContext context) : ITrainerService
{
    public async Task<ApiResponse<List<GetTrainerDto>>> GetAll()
    {
        var trainers = await context.Trainers
            .ToListAsync();
        var trainerDto = trainers.Select(t => new GetTrainerDto()
        {
            FirstName = t.FirstName,
            LastName = t.LastName,
            PhoneNumber = t.PhoneNumber,
            Experience = t.Experience,
            Status = t.Status,
            Specialization = t.Specialization,
        }).ToList();
        return new ApiResponse<List<GetTrainerDto>>(trainerDto);
    }

    public async Task<ApiResponse<Trainer>> GetById(int id)
    {
        var trainer = await context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
        return trainer == null
            ? new ApiResponse<Trainer>(HttpStatusCode.InternalServerError, "Trainer not Created")
            : new ApiResponse<Trainer>(trainer);
    }

    public async Task<ApiResponse<string>> Create(CreateTrainerDto trainer)
    {
        var trainerDto = new Trainer()
        {
            FirstName = trainer.FirstName,
            LastName = trainer.LastName,
            PhoneNumber = trainer.PhoneNumber,
            Experience = trainer.Experience,
            Status = trainer.Status,
            Specialization = trainer.Specialization
        };
        context.Trainers.AddAsync(trainerDto);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Trainer not Created")
            : new ApiResponse<string>("Created Succesfuly");
    }

    public async Task<ApiResponse<string>> Update(UpdateTrainerDto trainer)
    {
        var existingTrainer = await context.Trainers.FirstOrDefaultAsync(t => t.Id == trainer.Id);
        if (existingTrainer == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Trainer Not Found");
        }

        existingTrainer.FirstName = trainer.FirstName;
        existingTrainer.LastName = trainer.LastName;
        existingTrainer.PhoneNumber = trainer.PhoneNumber;
        existingTrainer.Experience = trainer.Experience;
        existingTrainer.Status = trainer.Status;
        existingTrainer.Specialization = trainer.Specialization;
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Trainer not updated")
            : new ApiResponse<string>("Updated succesfully");
    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        var existingTrainer = await context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTrainer == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Trainer Not Found");
        }

        context.Trainers.Remove(existingTrainer);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Trainer not Deleted")
            : new ApiResponse<string>("Deleted");
    }
}