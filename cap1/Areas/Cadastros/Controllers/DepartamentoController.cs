using cap1.Data;
using cap1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace cap1.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    public class DepartamentoController : Controller
    {
        private readonly IESContext _Context;
        public DepartamentoController(IESContext context)
        {
            _Context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _Context.Departamentos.Include(i => i.Instituicao).OrderBy(c => c.Nome).ToListAsync());
        }
        public ActionResult Create()
        {
            var instituicoes = _Context.Instituicoes.ToList();
            instituicoes.Insert(0, new Instituicao() { InstituicaoID = 0, Nome = "Selecione a Instituição" });
            ViewBag.Instituicoes = instituicoes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome, InstituicaoID")] Departamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _Context.Add(departamento);
                    await _Context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(departamento);
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var departamento = await _Context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);

            if (departamento == null) return NotFound();
            ViewBag.Instituicoes = new SelectList(_Context.Instituicoes.OrderBy(m => m.Nome),
                "InstituicaoID", "Nome", departamento.InstituicaoID);
            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, [Bind("DepartamentoID, Nome, InstituicaoID")] Departamento departamento)
        {
            if (id != departamento.DepartamentoID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _Context.Update(departamento);
                    await _Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.DepartamentoID)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Instituicoes = new SelectList(_Context.Instituicoes.OrderBy(m => m.Nome), "InstituicaoID", "Nome",
                departamento?.InstituicaoID);
            return View(departamento);
        }
        private bool DepartamentoExists(int? id)
        {
            return _Context.Departamentos.Any(e => e.DepartamentoID == id);
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var departamento = await _Context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            _Context.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();
            if (departamento == null) return NotFound();
            return View(departamento);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var departamento = await _Context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            _Context.Instituicoes.Where(i => departamento.InstituicaoID == i.InstituicaoID).Load();
            if (departamento == null) return NotFound();
            return View(departamento);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            var departamento = await _Context.Departamentos.SingleOrDefaultAsync(m => m.DepartamentoID == id);
            _Context.Departamentos.Remove(departamento);
            TempData["Message"] = "Departamento " + departamento.Nome.ToUpper() + " foi removida";
            await _Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
