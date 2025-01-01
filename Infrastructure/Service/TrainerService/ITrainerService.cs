using Domain.DTO_s.TrainerDto;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Service.TrainerService;

public interface ITrainerService
{
    Task<ApiResponse<List<GetTrainerDto>>> GetAll();
    Task<ApiResponse<Trainer>> GetById(int id);
    Task<ApiResponse<string>> Create(CreateTrainerDto trainer);
    Task<ApiResponse<string>> Update(UpdateTrainerDto trainer);
    Task<ApiResponse<string>> Delete(int id);
}