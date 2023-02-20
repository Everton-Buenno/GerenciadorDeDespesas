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
    public class TipoDespesasController : Controller
    {
        private readonly Context _context;

        public TipoDespesasController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TiposDespesas.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string txtProcurar)
        {
            if(!String.IsNullOrEmpty(txtProcurar))
            {
                return View(await _context.TiposDespesas.Where(td => td.Nome.ToUpper().Contains(txtProcurar.ToUpper())).ToListAsync());  
            }

            return View( await _context.TiposDespesas.ToListAsync());

        }

        public async Task<JsonResult> TipoDespesaExist(string Nome)
        {
            if (await _context.TiposDespesas.AnyAsync(td => td.Nome.ToUpper() == Nome.ToUpper()))
                return Json("Esse tipo de despesa ja existe!");


            return Json(true);
        }

         public JsonResult AdicionarTipoDespesa(string txtDespesa)
        {
            if (!String.IsNullOrEmpty(txtDespesa))
            {
                if(!_context.TiposDespesas.Any(td => td.Nome.ToUpper() == txtDespesa.ToUpper()))
                {
                    TipoDespesa tipoDespesa = new TipoDespesa();
                    tipoDespesa.Nome = txtDespesa;
                    _context.Add(tipoDespesa);
                    _context.SaveChanges();
                    return Json (true);
                }
            }

            return Json(false);
        }   
    
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoDespesaId,Nome")] TipoDespesa tipoDespesa)
        {
        

            if (ModelState.IsValid)
            {
               

                TempData["Confirmacao"] = tipoDespesa.Nome + " foi cadastrado com sucesso.";


                _context.Add(tipoDespesa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDespesa);
        }

        // GET: TipoDespesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TiposDespesas == null)
            {
                return NotFound();
            }

            var tipoDespesa = await _context.TiposDespesas.FindAsync(id);
            if (tipoDespesa == null)
            {
                return NotFound();
            }
            return View(tipoDespesa);
        }

        // POST: TipoDespesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoDespesaId,Nome")] TipoDespesa tipoDespesa)
        {
            if (id != tipoDespesa.TipoDespesaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["Confirmacao"] = tipoDespesa.Nome + "foi atualizado com sucesso.";

                    _context.Update(tipoDespesa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDespesaExists(tipoDespesa.TipoDespesaId))
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
            return View(tipoDespesa);
        }




        [HttpPost]

        public async Task<JsonResult> Delete(int id)
        {

            var tipoDespesa = await _context.TiposDespesas.FindAsync(id);
            TempData["Confirmacao"] = tipoDespesa.Nome + " foi excluido com sucesso.";
            if (tipoDespesa != null)
            {
                
                _context.TiposDespesas.Remove(tipoDespesa);
            }
           
            await _context.SaveChangesAsync();
            return Json(tipoDespesa.Nome + "excluido com sucesso.");
        }

        private bool TipoDespesaExists(int id)
        {
            return _context.TiposDespesas.Any(e => e.TipoDespesaId == id);
        }
    }
}
