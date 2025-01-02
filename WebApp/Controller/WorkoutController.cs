using Domain.DTO_s.WorkoutDto;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Service.WorkoutService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class WorkoutController(IWorkoutService service)
{
    [HttpGet]
    public async Task<ApiResponse<List<GetWorkoutDto>>> Getall() => await service.GetAll();
    
    [HttpGet("{id}")]
    public async Task<ApiResponse<Workout>> GetById(int id) => await service.GetById(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create(CreateWorkoutDto workout) => await service.Create(workout);

    [HttpPut]
    public async Task<ApiResponse<string>> Update(UpdateWorkoutDto workout) => await service.Update(workout);

    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.Delete(id);
}