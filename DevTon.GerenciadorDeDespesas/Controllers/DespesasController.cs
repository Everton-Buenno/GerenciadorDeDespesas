using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevTon.GerenciadorDeDespesas.Models;
using X.PagedList;
using DevTon.GerenciadorDeDespesas.ViewModels;

namespace DevTon.GerenciadorDeDespesas.Controllers
{
    public class DespesasController : Controller
    {
        private readonly Context _context;

        public DespesasController(Context context)
        {
            _context = context;
        }

     
        public async Task<IActionResult> Index(int? page)
        {
            const int itensPage = 10;
            int numberPage = (page ?? 1);


            ViewData["Meses"] = new SelectList(_context.Meses.Where(m => m.MesId == m.Salarios.MesId), "MesId", "Nome");
            var context = _context.Despesas.Include(d => d.Meses).Include(d => d.TipoDespesa).OrderBy(d => d.MesId);
            return View(await context.ToPagedListAsync(numberPage,itensPage));
        }

       

        public IActionResult Create()
        {
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome");
            ViewData["TipoDespesaId"] = new SelectList(_context.TiposDespesas, "TipoDespesaId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DespesaId,MesId,TipoDespesaId,Valor")] Despesa despesa)
        {
            if (ModelState.IsValid)
            {
                TempData["Confirmacao"] = "Despesa Cadastrada com sucesso.";
                _context.Add(despesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesa.MesId);
            ViewData["TipoDespesaId"] = new SelectList(_context.TiposDespesas, "TipoDespesaId", "Nome", despesa.TipoDespesaId);
            return View(despesa);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Despesas == null)
            {
                return NotFound();
            }

            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null)
            {
                return NotFound();
            }
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesa.MesId);
            ViewData["TipoDespesaId"] = new SelectList(_context.TiposDespesas, "TipoDespesaId", "Nome", despesa.TipoDespesaId);
            return View(despesa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DespesaId,MesId,TipoDespesaId,Valor")] Despesa despesa)
        {
            if (id != despesa.DespesaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmacao"] = "Despesa atualizada com sucesso.";
                    _context.Update(despesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DespesaExists(despesa.DespesaId))
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
            ViewData["MesId"] = new SelectList(_context.Meses, "MesId", "Nome", despesa.MesId);
            ViewData["TipoDespesaId"] = new SelectList(_context.TiposDespesas, "TipoDespesaId", "Nome", despesa.TipoDespesaId);
            return View(despesa);
        }

       

     
        [HttpPost]
     
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Despesas == null)
            {
                return Problem("Entity set 'Context.Despesas'  is null.");
            }
            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa != null)
            {
                _context.Despesas.Remove(despesa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DespesaExists(int id)
        {
          return _context.Despesas.Any(e => e.DespesaId == id);
        }


        public JsonResult GastosTotaisMes(int mesId)
        {
            GastosTotaisMesViewModel gastos = new GastosTotaisMesViewModel();

            gastos.ValorTotalGasto = _context.Despesas.Where(d => d.Meses.MesId == mesId).Sum(d => d.Valor);
            gastos.Salario = _context.Salario.Where(s => s.Meses.MesId == mesId).Select(s => s.Valor).FirstOrDefault();

            return Json(gastos);
        }

        public JsonResult GastoMes(int mesId)
        {
            var query = from despesas in _context.Despesas
                        where despesas.Meses.MesId == mesId
                        group despesas by despesas.TipoDespesa.Nome into g
                        select new
                        {
                            TiposDespesas = g.Key,
                            Valores = g.Sum(d => d.Valor)
                        };

            return Json(query);
        }

        public JsonResult GastosTotais()
        {
            var query = _context.Despesas
                .Include(m => m.Meses)
                .ToList()
                .OrderBy(m => m.Meses.MesId)
                .GroupBy(m => m.Meses.MesId)
                .Select(d => new { NomeMeses = d.Select(x => x.Meses.Nome).Distinct(), Valores = d.Sum(x => x.Valor) });


            return Json(query);
        }
    }
}
