namespace LazyBookworm.Common.Models.Common
{
    public class LoginDetails
    {
        public string Username { get; set; }
        public string Password { get; private set; }

        public LoginDetails(string username, string password)
        {
            Username = username;
            SetPassword(password);
        }

        public void SetPassword(string password)
        {
            //TODO Secure Password
            Password = password;
        }
    }
}