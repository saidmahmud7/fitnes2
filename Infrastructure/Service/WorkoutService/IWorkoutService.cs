using Domain.DTO_s.TrainerDto;
using Domain.DTO_s.WorkoutDto;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Service.WorkoutService;

public interface IWorkoutService
{
    Task<ApiResponse<List<GetWorkoutDto>>> GetAll();
    Task<ApiResponse<Workout>> GetById(int id);
    Task<ApiResponse<string>> Create(CreateWorkoutDto workout);
    Task<ApiResponse<string>> Update(UpdateTrainerDto workout);
    Task<ApiResponse<string>> Delete(int id);
}