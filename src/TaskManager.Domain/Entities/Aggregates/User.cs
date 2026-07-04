using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Common;

namespace TaskManager.Domain.Entities.Aggregates;

public class User:BaseEntity
{
    private User() { }
    protected User(string name, string email, string passwordHash)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        SetCreatedAt();
    }
    public string Name { get;private set; }
    public string Email { get;private set; }
    public string PasswordHash { get;private set; }
    //private readonly List<Project> _projects = new();
    //public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();
    public static User CreateUser(string name, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password cannot be empty");
        
        return new User(name, email, passwordHash);

    }
    public void ChangeEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email Can Not be Null");
        if (Email == email)
            throw new ArgumentException("New email Can not be same as old one");
        Email= email;
        SetUpdatedAt();
    }
    public void ChangePassword(string currentPasswordHash, string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("Password cannot be empty");

        if (PasswordHash != currentPasswordHash)
            throw new InvalidOperationException("Current password is incorrect");

        if (PasswordHash == newPasswordHash)
            throw new InvalidOperationException("New password cannot be same as current password");

        PasswordHash = newPasswordHash;
        SetUpdatedAt();
    }
    public void DeleteAccount()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Account is already deleted");

        SetDeleted(); 
    }

}