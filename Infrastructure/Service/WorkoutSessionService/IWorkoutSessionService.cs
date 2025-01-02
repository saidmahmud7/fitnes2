using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Service.WorkoutSessionService;

public interface IWorkoutSessionService
{
    Task<ApiResponse<List<GetWorkoutSessionDto>>> GetAll();
    Task<ApiResponse<WorkoutSession>> GetById(int id);
    Task<ApiResponse<string>> Create(CreateWorkoutSessionDto session);
    Task<ApiResponse<string>> Updated(UpdatedSessionDto session);
    Task<ApiResponse<string>> Delete(int id);
}