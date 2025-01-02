using Domain.DTO_s.ClientDto;
using Domain.DTO_s.WorkoutDto;
using Domain.Entities;
using Infrastructure.Response;
using Infrastructure.Service.ClientService;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controller;


[ApiController]
[Route("api/[controller]")]
public class ClientController(IClientService service)
{
    [HttpGet]
    public async Task<ApiResponse<List<GetClientDto>>> Getall() => await service.GetAll();
    
    [HttpGet("{id}")]
    public async Task<ApiResponse<Client>> GetById(int id) => await service.GetById(id);

    [HttpPost]
    public async Task<ApiResponse<string>> Create(CreateClientDto client) => await service.Create(client);

    [HttpPut]
    public async Task<ApiResponse<string>> Update(UpdateClientDto client) => await service.Update(client);

    [HttpDelete]
    public async Task<ApiResponse<string>> Delete(int id) => await service.Delete(id);
}