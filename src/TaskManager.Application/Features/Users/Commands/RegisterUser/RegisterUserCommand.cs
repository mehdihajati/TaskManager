namespace TaskManager.Application.Features.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Name, string Email, string Password);
//record replaced for this bunch of codesand I think recod is enough for what we want to do
// Traditional class - lots of boilerplate for simple data
/*public class RegisterUserCommand
{
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }

    public RegisterUserCommand(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
}*/