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
    public class FCTController : Controller
    {
        private readonly DataContext _context;

        public FCTController(DataContext context)
        {
            _context = context;
        }

        // GET: FCT
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.FCT
                .Include(f => f.SucursalOrigen)
                .Include(f => f.SucursalDestino)
                .Include(f => f.Destinatario)
                .Include(f => f.Remitente);
            return View(await dataContext.ToListAsync());
        }
        public IActionResult Search()
        {
            return View();
        }

        // GET: FCT/ByRemitente
        public async Task<IActionResult> SearchFunc(int? remitenteId)
        {
            if (remitenteId == null || _context.FCT == null)
            {
                return NotFound();
            }

            var fCT = await _context.FCT
                .Include(f => f.SucursalOrigen)
                .Include(f => f.SucursalDestino)
                             .Include(f => f.Destinatario)
                 .Include(f => f.Remitente)
                .Where(m => m.ClienteId == remitenteId).ToListAsync();
            if (fCT == null)
            {
                return NotFound();
            }

            return View("Index", fCT);
        }


        public IActionResult SearchDest()
        {
            return View();
        }

        // GET: FCT/ByRemitente
        public async Task<IActionResult> SearchDestFunc(int? destinatarioId)
        {
            if (destinatarioId == null || _context.FCT == null)
            {
                return NotFound();
            }

            var fCT = await _context.FCT
                .Include(f => f.SucursalOrigen)
                .Include(f => f.SucursalDestino)
                .Include(f => f.Destinatario)
                 .Include(f => f.Remitente)
                .Where(m => m.DestinatarioId == destinatarioId).ToListAsync();
            if (fCT == null)
            {
                return NotFound();
            }

            return View("Index", fCT);
        }




        // GET: FCT/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FCT == null)
            {
                return NotFound();
            }

            var fCT = await _context.FCT
                .Include(f => f.SucursalOrigen)
                 .Include(f => f.Destinatario)
                 .Include(f => f.Remitente)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (fCT == null)
            {
                return NotFound();
            }

            return View(fCT);
        }

        // GET: FCT/Create
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
            ViewData["DestinatarioId"] = new SelectList(clientes, "Id", "NombreCompleto");
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Nombre");
            return View();
        }

        // POST: FCT/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,IdSucursalDestino,Valor,SucursalId,DestinatarioId")] FCT fCT)
        {
            fCT.Codigo = _context.NTabla("Documento");
            fCT.TipoDocumento = "FCT";
            fCT.FechaRecaudo = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(fCT);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            TempData["alertMessage"] = "Error con el modelo";
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Id", fCT.SucursalId);
            return View(fCT);
        }

        // GET: FCT/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FCT == null)
            {
                return NotFound();
            }

            var fCT = await _context.FCT.FindAsync(id);
            if (fCT == null)
            {
                return NotFound();
            }
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Id", fCT.SucursalId);
            return View(fCT);
        }

        // POST: FCT/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DestinatarioId,IdSucursalDestino,Codigo,TipoDocumento,Valor,FechaRecaudo,SucursalId,ClienteId")] FCT fCT)
        {
            if (id != fCT.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fCT);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FCTExists(fCT.Codigo))
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
            ViewData["SucursalId"] = new SelectList(_context.Sucursal, "Id", "Id", fCT.SucursalId);
            return View(fCT);
        }

        // GET: FCT/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FCT == null)
            {
                return NotFound();
            }

            var fCT = await _context.FCT
                .Include(f => f.SucursalOrigen)
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (fCT == null)
            {
                return NotFound();
            }

            return View(fCT);
        }

        // POST: FCT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FCT == null)
            {
                return Problem("Entity set 'DataContext.FCT'  is null.");
            }
            var fCT = await _context.FCT.FindAsync(id);
            if (fCT != null)
            {
                _context.FCT.Remove(fCT);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FCTExists(int id)
        {
            return (_context.FCT?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
