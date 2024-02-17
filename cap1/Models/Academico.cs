using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace cap1.Models
{
    public class Academico
    {
        public int AcademicoID { get; set; }

        [StringLength(10, MinimumLength = 10)]
        //[RegularExpression("[0-9] {10}")]
        [DisplayName("Registro Academico")]
        [Required]
        public string? RegistroAcademico { get; set; }

        [Required]
        public string? Nome {  get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [Required]
        public DateTime? Nascimento { get; set; }
    }
}
