using System.Net;
using Domain.DTO_s.ClientDto;
using Domain.DTO_s.WorkoutSessionDto;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Response;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Service.ClientService;

public class ClientService(DataContext context) : IClientService
{
    public async Task<ApiResponse<List<GetClientDto>>> GetAll()
    {
        var client = context.Clients
            .Include(x => x.WorkoutSessions)
            .AsQueryable();
        var clientDto = context.Clients.Select(c => new GetClientDto()
        {
            FirstName = c.FirstName,
            LastName = c.LastName,
            PhoneNumber = c.PhoneNumber,
            Email = c.Email,
            DateOfBirth = c.DateOfBirth,
            MembershipStatus = c.MembershipStatus,
            WorkoutSessions = c.WorkoutSessions.Select(s => new GetWorkoutSessionDto()
            {
                SessionDate = s.SessionDate,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Status = s.Status,
                Comment = s.Comment,
                MaxCapacity = s.MaxCapacity,
                CurrentParticipants = s.CurrentParticipants,
                CreatedAt = s.CreatedAt,
            }).ToList()
        }).ToList();
        return new ApiResponse<List<GetClientDto>>(clientDto);
    }

    public async Task<ApiResponse<Client>> GetById(int id)
    {
        var client = await context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        return client == null
            ? new ApiResponse<Client>(HttpStatusCode.InternalServerError, "Client not Created")
            : new ApiResponse<Client>(client);
    }

    public async Task<ApiResponse<string>> Create(CreateClientDto client)
    {
        var clients = new Client()
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            PhoneNumber = client.PhoneNumber,
            Email = client.Email,
            DateOfBirth = client.DateOfBirth,
            MembershipStatus = client.MembershipStatus,
        };
        context.Clients.AddAsync(clients);
        var res = await context.SaveChangesAsync();
        return res == 0
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Client not Created")
            : new ApiResponse<string>("Created Succesfuly");
    }

    public async Task<ApiResponse<string>> Update(UpdateClientDto client)
    {
        var existingClient = await context.Clients.FirstOrDefaultAsync(c => c.Id == client.Id);
        if (existingClient == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Client not Found");
        }

        existingClient.FirstName = client.FirstName;
        existingClient.LastName = client.LastName;
        existingClient.PhoneNumber = client.PhoneNumber;
        existingClient.Email = client.Email;
        existingClient.DateOfBirth = client.DateOfBirth;
        existingClient.MembershipStatus = client.MembershipStatus;
        var res = await context.SaveChangesAsync();
        return res == null
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.Created, "Client Updated");
    }

    public async Task<ApiResponse<string>> Delete(int id)
    {
        var existingClient = await context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        if (existingClient == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Client not Found");
        }

        context.Clients.Remove(existingClient);
        var res = await context.SaveChangesAsync();
        return res == null
            ? new ApiResponse<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new ApiResponse<string>(HttpStatusCode.Created, "Client Deleted");
    }
}