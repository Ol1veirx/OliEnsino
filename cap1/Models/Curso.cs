namespace cap1.Models
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Nome { get; set; }

        public int DepartamentoID { get; set; }
        public Departamento Departamento { get; set; }

        public virtual ICollection<CursoDisciplina>? CursosDisciplinas { get; set; }
    }
}
