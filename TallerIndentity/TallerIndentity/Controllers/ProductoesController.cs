    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using TallerIndentity.Models;

    namespace TallerCrud.Controllers
    {
        public class ProductoesController : Controller
        {
            private readonly TallerIdentityContext _context;

            public ProductoesController(TallerIdentityContext context)
            {
                _context = context;
            }

            [Authorize]
            // GET: Productoes
            public async Task<IActionResult> Index(string sortOrder, string searchString)
            {
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["CurrentFilter"] = searchString;

                var productos = from s in _context.Productos
                                select s;
                if (!String.IsNullOrEmpty(searchString))
                {
                    productos = productos.Where(s => s.Nombre.Contains(searchString)
                        || s.Nombre.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        productos = productos.OrderByDescending(s => s.Nombre);
                        break;
                    default:
                        productos = productos.OrderBy(s => s.Nombre);
                        break;
                }
                return View(await productos.AsNoTracking().ToListAsync());
            }

            // GET: Productoes/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Productos == null)
                {
                    return NotFound();
                }

                var producto = await _context.Productos
                    .FirstOrDefaultAsync(m => m.IdProductos == id);
                if (producto == null)
                {
                    return NotFound();
                }

                return View(producto);
            }

            // GET: Productoes/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Productoes/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("IdProductos,Codigo,Nombre,Precio,Cantidad")] Producto producto)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(producto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(producto);
            }

            // GET: Productoes/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null || _context.Productos == null)
                {
                    return NotFound();
                }

                var producto = await _context.Productos.FindAsync(id);
                if (producto == null)
                {
                    return NotFound();
                }
                return View(producto);
            }

            // POST: Productoes/Edit/5
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("IdProductos,Codigo,Nombre,Precio,Cantidad")] Producto producto)
            {
                if (id != producto.IdProductos)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(producto);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductoExists(producto.IdProductos))
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
                return View(producto);
            }

            // GET: Productoes/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Productos == null)
                {
                    return NotFound();
                }

                var producto = await _context.Productos
                    .FirstOrDefaultAsync(m => m.IdProductos == id);
                if (producto == null)
                {
                    return NotFound();
                }

                return View(producto);
            }

            // POST: Productoes/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Productos == null)
                {
                    return Problem("Entity set 'TallerCrudContext.Productos'  is null.");
                }
                var producto = await _context.Productos.FindAsync(id);
                if (producto != null)
                {
                    _context.Productos.Remove(producto);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ProductoExists(int id)
            {
                return (_context.Productos?.Any(e => e.IdProductos == id)).GetValueOrDefault();
            }
        }
    }
