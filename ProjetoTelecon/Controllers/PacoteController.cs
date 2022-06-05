using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetoTelecon.Data;
using ProjetoTelecon.Models;

namespace ProjetoTelecon.Controllers
{
    public class PacoteController : Controller
    {
        private readonly Context _context;

        public PacoteController(Context context)
        {
            _context = context;
        }

        // GET: Pacote
        public async Task<IActionResult> Index()
        {
            var context = _context.Pacote.Include(p => p.Servico);
            return View(await context.ToListAsync());
        }

        // GET: Pacote/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pacote == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacote
                .Include(p => p.Servico)
                .FirstOrDefaultAsync(m => m.PacoteId == id);
            if (pacote == null)
            {
                return NotFound();
            }

            return View(pacote);
        }

        // GET: Pacote/Create
        public IActionResult Create()
        {
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "ServicoId");
            return View();
        }

        // POST: Pacote/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacoteId,Nome,Mensalidade,Ativo,DataModificacao,DataCriacao,ServicoId")] Pacote pacote)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacote);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "ServicoId", pacote.ServicoId);
            return View(pacote);
        }

        // GET: Pacote/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pacote == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacote.FindAsync(id);
            if (pacote == null)
            {
                return NotFound();
            }
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "ServicoId", pacote.ServicoId);
            return View(pacote);
        }

        // POST: Pacote/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacoteId,Nome,Mensalidade,Ativo,DataModificacao,DataCriacao,ServicoId")] Pacote pacote)
        {
            if (id != pacote.PacoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacoteExists(pacote.PacoteId))
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
            ViewData["ServicoId"] = new SelectList(_context.Servico, "ServicoId", "ServicoId", pacote.ServicoId);
            return View(pacote);
        }

        // GET: Pacote/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pacote == null)
            {
                return NotFound();
            }

            var pacote = await _context.Pacote
                .Include(p => p.Servico)
                .FirstOrDefaultAsync(m => m.PacoteId == id);
            if (pacote == null)
            {
                return NotFound();
            }

            return View(pacote);
        }

        // POST: Pacote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pacote == null)
            {
                return Problem("Entity set 'Context.Pacote'  is null.");
            }
            var pacote = await _context.Pacote.FindAsync(id);
            if (pacote != null)
            {
                _context.Pacote.Remove(pacote);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacoteExists(int id)
        {
          return (_context.Pacote?.Any(e => e.PacoteId == id)).GetValueOrDefault();
        }
    }
}
