using AuthenticationAndAuthorization_hw.Logic;
using AuthenticationAndAuthorization_hw.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization_hw.Controllers
{
    public class NotesController : Controller
    {
        private PseudoDatabase _db;

        public NotesController(PseudoDatabase db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Add(NoteBinding model)
        {
            var currentUserId = _db.Users.FirstOrDefault(u => u.Login == User.Identity.Name).Id;
            if (currentUserId == null)
            {
                throw new NullReferenceException("can't find user with such name like authorized user");
            }
            _db.Notes.Add(
                new Logic.Notes.Note
                {
                    Tittle = model.Tittle,
                    Content = model.Content,
                    OwnerId = currentUserId
                });
            return View();
        }
        [Authorize]
        public IActionResult All()
        {
            var currentUserId = _db.Users.FirstOrDefault(u => u.Login == User.Identity.Name).Id;
            if (currentUserId == null)
            {
                throw new NullReferenceException("can't find user with such name like authorized user");
            }
            var currentUserNotes = _db.Notes.Where(n => n.OwnerId == currentUserId).ToList();
            return View(currentUserNotes);
        }
    }
}
