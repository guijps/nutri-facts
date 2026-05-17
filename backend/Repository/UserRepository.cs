public class UserRepository
{
    private readonly List<AppUser> _users = new List<AppUser>()
    {
        new AppUser
        {
            Id = Guid.NewGuid().ToString(),
            UserName = "testuser",
            Email = "testuser@example.com",
            PasswordHash = "passwordhash"
        }
    };

    public AppUser? GetByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public void AddUser(AppUser user)
    {
        _users.Add(user);
    }
}