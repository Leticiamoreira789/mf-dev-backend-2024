using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mf_dev_backend_2024.Models;
using Microsoft.AspNetCore.Authorization;

namespace mf_dev_backend_2024.Controllers
{
    public class VeiculosController : Controller
    {
        private readonly AppDbContext _context;
        public VeiculosController(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var dados = await _context.Veiculos.ToListAsync();
            return View(dados);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(Veiculo veiculo)
        {
            if(ModelState.IsValid)
            {
                _context.Veiculos.Add(veiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(veiculo);
        }

        public async Task<IActionResult> Edit (int? id)
        {
            if (id == null)
                return NotFound();

            var dados = await _context.Veiculos.FindAsync(id);
            if(dados == null)
                return NotFound();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Veiculo veiculo)
        {
            if (id != veiculo.Id)
                return NotFound();      
            if(ModelState.IsValid)
            {
                _context.Update(veiculo); 
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
