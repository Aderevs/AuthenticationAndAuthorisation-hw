using System.ComponentModel.DataAnnotations;

namespace AuthenticationAndAuthorization_hw.Models
{
    public class NoteBinding
    {
        public string Tittle {  get; set; }

        [Required]
        public string Content {  get; set; }
    }
}
