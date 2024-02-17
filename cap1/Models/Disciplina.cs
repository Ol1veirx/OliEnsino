namespace cap1.Models
{
    public class Disciplina
    {
        public int DisciplinaID { get; set; }
        public string Nome { get; set; }

        public virtual ICollection<CursoDisciplina>? CursosDisciplinas { get; set; }
    }
}
