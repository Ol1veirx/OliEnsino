using cap1.Data;
using cap1.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace cap1.Services
{
    public class AcademicoServices
    {
        private IESContext _context;
        public AcademicoServices(IESContext context)
        {
            _context = context;
        }
        public IQueryable<Academico> ObterAcademicoPorNome()
        {
            return _context.Academicos.OrderBy(a => a.Nome);
        }
        public async Task<Academico> ObterAcademicoPorID(int? id)
        {
            return await _context.Academicos.FindAsync(id);
        }
        public async Task<Academico> Create(Academico academico)
        {
            if (academico.AcademicoID <= 0)
            {
                _context.Academicos.Add(academico);
            }
            else
            {
                _context.Academicos.Update(academico);
            }
            await _context.SaveChangesAsync();
            return academico;
        }
        public async Task<Academico> Update(Academico academico)
        {
            var index = await ObterAcademicoPorID(academico.AcademicoID);
            if (index == null) return null;
            _context.Update(academico);
            await _context.SaveChangesAsync();
            return academico;
        }
        public async Task<Academico> DeleteAcademic(int? id)
        {
            Academico academico = await ObterAcademicoPorID(id);
            _context.Academicos.Remove(academico);
            await _context.SaveChangesAsync();
            return academico;
        }
    }
}
