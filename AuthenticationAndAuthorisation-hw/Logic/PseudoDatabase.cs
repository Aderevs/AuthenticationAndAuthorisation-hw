using AuthenticationAndAuthorization_hw.Logic.Notes;
using AuthenticationAndAuthorization_hw.Logic.Account;
using System.Security.Cryptography.X509Certificates;
using AuthenticationAndAuthorization_hw.Controllers;
namespace AuthenticationAndAuthorization_hw.Logic
{
    public class PseudoDatabase
    {
        public List<User> Users { get; set; }
        public List<Note> Notes { get; set; }
        public PseudoDatabase()
        {
            Users = new List<User>();
            Notes = new List<Note>();
            Users.Add(
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = "testUser",
                    Salt = Guid.NewGuid()
                });
            Users[0].PasswordHash = PasswordHasher.HashPassword("test Pass" + Users[0].Salt.ToString());
            Notes.Add(
                new Note
                {
                    Tittle = "First note",
                    Content = "content of the first note to check how it will looks on the web site",
                    OwnerId = Users[0].Id
                });
        }
    }
}
