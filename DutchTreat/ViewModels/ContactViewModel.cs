namespace DutchTreat.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContactViewModel
    {
        [Required]
        [MinLength(5, ErrorMessage = "Too short")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Too Long")]
        public string Message { get; set; }
    }
}
