namespace OrderSystem.Application.DTOs.User;

public record class UserDto
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }

    public string Telephone { get; set; } = string.Empty;

}
