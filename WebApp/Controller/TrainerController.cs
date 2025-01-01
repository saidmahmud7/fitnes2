using Domain.DTO_s.TrainerDto;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Service.TrainerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;


[ApiController]
[Route("api/[controller]")]
public class TrainerController(ITrainerService service)
{
    [HttpGet]
    public  async Task<ApiResponse<List<GetTrainerDto>>> GetAll() => await service.GetAll() ;

    [HttpGet("{id}")]
    public async Task<ApiResponse<Trainer>> GetById(int id) => await service.GetById(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create(CreateTrainerDto trainer) => await service.Create(trainer);

    [HttpPut]
    public async Task<ApiResponse<string>> Update(UpdateTrainerDto trainer) => await service.Update(trainer);

    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.Delete(id);
}