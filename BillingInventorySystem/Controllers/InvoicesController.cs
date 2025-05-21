using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillingInventorySystem.Data;
using BillingInventorySystem.Models;
using BillingInventorySystem.ViewModel;

namespace BillingInventorySystem.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Invoices.Include(i => i.Customer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new InvoiceFormViewModel
            {
                Customers = _context.Customers.ToList(),
                Products = _context.Products.ToList(),
                Items = new List<InvoiceItemViewModel> { new InvoiceItemViewModel() } // Start with one row
            };
            return View(viewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(InvoiceFormViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.Customers = _context.Customers.ToList();
        //        model.Products = _context.Products.ToList();
        //        return View(model);
        //    }

        //    var invoice = new Invoice
        //    {
        //        CustomerId = model.CustomerId,
        //        Date = model.Date,
        //        Items = model.Items.Select(i => new InvoiceItem
        //        {
        //            ProductId = i.ProductId,
        //            Quantity = i.Quantity,
        //            TotalPrice = _context.Products.Find(i.ProductId).UnitPrice * i.Quantity
        //        }).ToList()
        //    };

        //    _context.Invoices.Add(invoice);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        // POST: Invoices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Date,CustomerId")] Invoice invoice)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(invoice);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", invoice.CustomerId);
        //    return View(invoice);
        //}

        // GET: Invoices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "FullName", invoice.CustomerId);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InvoiceFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdowns before returning view
                model.Customers = _context.Customers.ToList();
                model.Products = _context.Products.ToList();
                return View(model);
            }

            try
            {
                // Create Invoice
                var invoice = new Invoice
                {
                    CustomerId = model.CustomerId,
                    Date = model.Date,
                    Items = new List<InvoiceItem>()
                };

                // Create InvoiceItems
                foreach (var item in model.Items)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
                    if (product == null)
                        continue;

                    var invoiceItem = new InvoiceItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        TotalPrice = product.UnitPrice * item.Quantity
                    };

                    invoice.Items.Add(invoiceItem);
                }

                // Save to DB
                _context.Invoices.Add(invoice);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving invoice: " + ex.Message);
                ModelState.AddModelError("", "An error occurred while saving the invoice.");
                model.Customers = _context.Customers.ToList();
                model.Products = _context.Products.ToList();
                return View(model);
            }
        }


        // GET: Invoices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice != null)
            {
                _context.Invoices.Remove(invoice);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
