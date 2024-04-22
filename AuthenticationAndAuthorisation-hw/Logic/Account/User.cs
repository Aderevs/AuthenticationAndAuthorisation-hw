namespace AuthenticationAndAuthorization_hw.Logic.Account
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login {  get; set; }
        public string PasswordHash { get; set; }
        public Guid Salt {  get; set; }

    }
}
