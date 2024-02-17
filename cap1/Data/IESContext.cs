using cap1.Models;
using cap1.Models.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace cap1.Data
{
    public class IESContext(DbContextOptions<IESContext> options) : IdentityDbContext<UsuarioDaAplicacao>(options)
    {
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoDisciplina>()
                            .HasKey(cd => new { cd.CursoID, cd.DisciplinaID });
            modelBuilder.Entity<CursoDisciplina>()
                            .HasOne(c => c.Curso)
                            .WithMany(cd => cd.CursosDisciplinas)
                            .HasForeignKey(c => c.CursoID);
            modelBuilder.Entity<CursoDisciplina>()
                            .HasOne(d => d.Disciplina)
                            .WithMany(cd => cd.CursosDisciplinas)
                            .HasForeignKey(d => d.DisciplinaID);
        }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Instituicao> Instituicoes {  get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set;}
        public DbSet<Academico> Academicos { get; set;}
    }
}
