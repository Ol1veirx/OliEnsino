﻿namespace cap1.Models
{
    public class CursoDisciplina
    {
        public int CursoID { get; set; }
        public Curso? Curso { get; set; }
        public int DisciplinaID { get; set; }

        public Disciplina? Disciplina { get; set; }
    }
}