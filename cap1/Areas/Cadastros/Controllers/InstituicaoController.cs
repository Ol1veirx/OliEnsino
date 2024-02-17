using System.ComponentModel;
using cap1.Data;
using cap1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace cap1.Areas.Cadastros.Controllers
{
    [Area("Cadastros")]
    [Authorize]
    public class InstituicaoController : Controller
    {
        private readonly IESContext _context;
        
        public InstituicaoController(IESContext context)
        {
            _context = context;
        }
        //Get
        public async Task<ActionResult> Index()
        {
            return View(await _context.Instituicoes.OrderBy(m => m.Nome).ToListAsync());
        } 

        //Get Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Nome, Endereco")] Instituicao instituicao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(instituicao);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao criar!", ex);
                }
            }
            return View(instituicao);
        }
        //GET Edit
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if(instituicao == null) return NotFound();
            return View(instituicao);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, [Bind("Nome, Endereco")]  Instituicao instituicao)
        {
            if (id == null) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(instituicao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine("Erro na alteração", ex);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(instituicao);
        }
        //GET Details
        public async Task<ActionResult> Details(int? id)
        {
            if(id == null) return NotFound();
            var instituicao = await _context.Instituicoes.Include(d => d.Departamentos).SingleOrDefaultAsync(m => m.InstituicaoID ==id);
            if (instituicao == null) return NotFound();
            return View(instituicao);
        }
        //GET Delete
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if (instituicao == null) return NotFound();
            return View(instituicao);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            var instituicao = await _context.Instituicoes.SingleOrDefaultAsync(m => m.InstituicaoID == id);
            if (instituicao == null) return NotFound();
            _context.Remove(instituicao);
            await _context.SaveChangesAsync();
            TempData["Message"] = "Instituição " + instituicao.Nome.ToUpper() + " foi removida";
            return RedirectToAction(nameof(Index));
        }
    }
}
