using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevTon.GerenciadorDeDespesas.Models;

namespace DevTon.GerenciadorDeDespesas.Controllers
{
    public class SalariosController : Controller
    {
        private readonly Context _context;

        public SalariosController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var context = _context.Salario.Include(s => s.Meses);
            return View(await context.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Index(string txtProcurar)
        {
            if (!String.IsNullOrEmpty(txtProcurar))
            {

                return View(await _context.Salario.Include(s => s.Meses).Where(m => m.Meses.Nome.ToUpper().Contains(txtProcurar.ToUpper())).ToListAsync());
            }

            return View(await _context.Salario.Include(s => s.Meses).ToListAsync());
        }

       
        public IActionResult Create()
        {
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId != s.Salarios.MesId), "MesId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalarioId,MesId,Valor")] Salario salario)
        {
            if (ModelState.IsValid)
            {

                TempData["Confirmacao"] = "Salário cadastrado com sucesso.";
                _context.Add(salario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId != s.Salarios.MesId), "MesId", "Nome", salario.MesId);
            return View(salario);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salario == null)
            {
                return NotFound();
            }

            var salario = await _context.Salario.FindAsync(id);
            if (salario == null)
            {
                return NotFound();
            }
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId == salario.MesId), "MesId", "Nome", salario.MesId);
            return View(salario);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalarioId,MesId,Valor")] Salario salario)
        {
            if (id != salario.SalarioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salario);
                    await _context.SaveChangesAsync();
                    TempData["Confirmacao"] = "Salário atualizado com sucesso.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalarioExists(salario.SalarioId))
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
            ViewData["MesId"] = new SelectList(_context.Meses.Where(s => s.MesId == salario.MesId), "MesId", "Nome", salario.MesId);
            return View(salario);
        }

       

        [HttpPost]
       
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Salario == null)
            {
                return Problem("Entity set 'Context.Salario'  is null.");
            }
            var salario = await _context.Salario.FindAsync(id);
            if (salario != null)
            {
                _context.Salario.Remove(salario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalarioExists(int id)
        {
          return _context.Salario.Any(e => e.SalarioId == id);
        }
    }
}
