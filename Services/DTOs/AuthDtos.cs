namespace SmartBabySitter.Services.DTOs;

public record RegisterRequestDto(string FullName, string Email, string Password, string Role);
public record LoginRequestDto(string Email, string Password);

public record AuthResponseDto(
    string Token,
    int UserId,
    string FullName,
    string Email,
    IList<string> Roles
);