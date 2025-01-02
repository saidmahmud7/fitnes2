using Domain.DTO_s.ClientDto;
using Domain.Entities;
using Infrastructure.Response;

namespace Infrastructure.Service.ClientService;

public interface IClientService
{
    Task<ApiResponse<List<GetClientDto>>> GetAll();
    Task<ApiResponse<Client>> GetById(int id);
    Task<ApiResponse<string>> Create(CreateClientDto client);
    Task<ApiResponse<string>> Update(UpdateClientDto client);
    Task<ApiResponse<string>> Delete(int id);
}