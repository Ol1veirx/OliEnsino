using System.ComponentModel.DataAnnotations;

namespace cap1.Models.Infra
{
    public class AcessarViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Lembra de mim?")]
        public bool LembrarDeMim {  get; set; }
    }
}
