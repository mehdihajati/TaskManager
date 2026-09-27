namespace TaskManager.API.Contracts.Requests;

public record ChangePasswordRequest(string CurrentPassword, string NewPassword);
