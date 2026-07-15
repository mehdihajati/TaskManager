namespace TaskManager.Application.Features.Users.DTOs;

public record UserDTO(Guid Id, String Name, string Email, DateTimeOffset CreatedAt);