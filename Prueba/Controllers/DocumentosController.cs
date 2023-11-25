using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Prueba.Context;
using Prueba.Models;

namespace Prueba.Controllers
{
    public class DocumentosController : Controller
    {
        private readonly DataContext _context;

        public DocumentosController(DataContext context)
        {
            _context = context;
        }

        // GET: Documentos
        public async Task<IActionResult> Index()
        {
            if (_context.Documento == null)
            {
                return NotFound();
            }
            var dataContext = _context.Documento.Include(d => d.SucursalOrigen);
            return View(await dataContext.ToListAsync());
        }

        // GET: Documentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Documento == null)
            {
                return NotFound();
            }

            var documento = await _context.Documento
                .Include(d => d.SucursalOrigen)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // GET: Documentos/Create
        public IActionResult Create()
        {
            var clientes = _context.Set<Cliente>()
                                      .Select(s => new
                                      {
                                          Id = s.Id,
                                          NombreCompleto = $"{s.Nombre} {s.Apellido}"
                                      })
                                      .ToList();

            ViewData["ClienteId"] = new SelectList(clientes, "Id", "NombreCompleto");
            ViewData["SucursalId"] = new SelectList(_context.Set<Sucursal>(), "Id", "Nombre");
            return View();
        }

        // POST: Documentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,TipoDocumento,Valor,FechaRecaudo,SucursalId,ClienteId")] Documento documento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(documento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
         
            ViewData["SucursalId"] = new SelectList(_context.Set<Sucursal>(), "Id", "Id", documento.SucursalId);
            return View(documento);
        }

        // GET: Documentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Documento == null)
            {
                return NotFound();
            }

            var documento = await _context.Documento.FindAsync(id);
            if (documento == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Set<Sucursal>(), "Id", "Id", documento.SucursalId);
            return View(documento);
        }

        // POST: Documentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Codigo,TipoDocumento,Valor,FechaRecaudo,SucursalId,ClienteId")] Documento documento)
        {
            if (id != documento.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(documento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentoExists(documento.Codigo))
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
            ViewData["SucursalId"] = new SelectList(_context.Set<Sucursal>(), "Id", "Id", documento.SucursalId);
            return View(documento);
        }

        // GET: Documentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Documento == null)
            {
                return NotFound();
            }

            var documento = await _context.Documento
                .Include(d => d.SucursalOrigen)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (documento == null)
            {
                return NotFound();
            }

            return View(documento);
        }

        // POST: Documentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Documento == null)
            {
                return Problem("Entity set 'DataContext.Documento'  is null.");
            }
            var documento = await _context.Documento.FindAsync(id);
            if (documento != null)
            {
                _context.Documento.Remove(documento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentoExists(int id)
        {
            return (_context.Documento?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
