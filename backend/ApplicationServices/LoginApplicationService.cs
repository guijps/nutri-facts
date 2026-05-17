public class LoginApplicationService
{
    private readonly UserRepository _userRepository;

    public LoginApplicationService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public AppUser? Login(string email, string password)
    {
        var user = _userRepository.GetByEmail(email);

        if (user == null)
            return null;

        // In a real application, you would hash the password and compare it
        if (user.PasswordHash != password)
            return null;

        return user;
    }
}