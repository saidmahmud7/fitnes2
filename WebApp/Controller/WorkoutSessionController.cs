using Domain.DTO_s.WorkoutDto;
using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Service.WorkoutSessionService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;
[ApiController]
[Route("api/[controller]")]
public class WorkoutSessionController(IWorkoutSessionService service)
{
    [HttpGet]
    public async Task<ApiResponse<List<GetWorkoutSessionDto>>> Getall() => await service.GetAll();
    
    [HttpGet("{id}")]
    public async Task<ApiResponse<WorkoutSession>> GetById(int id) => await service.GetById(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create(CreateWorkoutSessionDto workout) => await service.Create(workout);

    [HttpPut]
    public async Task<ApiResponse<string>> Update(UpdatedSessionDto workout) => await service.Updated(workout);

    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.Delete(id);
}