using cap1.Data;
using Microsoft.AspNetCore.Mvc;
using cap1.Services;
using Microsoft.EntityFrameworkCore;
using cap1.Models;

namespace cap1.Areas.Discente.Controllers
{
    [Area("Discente")]
    public class AcademicoController : Controller
    {
        private readonly IESContext _context;
        private readonly AcademicoServices services;

        public AcademicoController(IESContext context)
        {
            _context = context;
            services = new AcademicoServices(context);
        }

        public async Task<IActionResult> Index()
        {
            return View(await services.ObterAcademicoPorNome().ToListAsync());
        }
        private async Task<IActionResult> GetById(int? id)
        {
            if (id == null) return NotFound();
            var academico = await services.ObterAcademicoPorID(id.Value);
            if (academico == null) return NotFound();
            return View(academico);
        }
        public async Task<IActionResult> Details(int? id)
        {
            var academico = await GetById(id);
            if (academico == null) return NotFound();
            return View(academico);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            return await GetById(id);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return await GetById(id);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome, RegistroAcademico, Nascimento")] Academico academico)
        {
            try
            {
                await services.Create(academico);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possivel inserir os dados.");
            }
            return View(academico);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("AcademicoID, Nome, RegistroAcademico, Nascimento")] Academico academico)
        {
            if (id == null) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    await services.Create(academico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await AcademicoExists(academico.AcademicoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(academico);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var academico = await services.DeleteAcademic(id);
            return RedirectToAction(nameof(Index));
        }
        private async Task<bool> AcademicoExists(int? id)
        {
            return await services.ObterAcademicoPorID((int)id) != null;
        }
    }
}
