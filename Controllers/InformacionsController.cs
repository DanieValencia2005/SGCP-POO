﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SGCP_POO.Models;

namespace SGCP_POO.Controllers
{
    public class InformacionsController : Controller
    {
        private readonly SGCPContext _context;

        public InformacionsController(SGCPContext context)
        {
            _context = context;
        }

        // GET: Informacions/Actualizar
        public async Task<IActionResult> Actualizar()
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");

            if (idEstudiante == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var info = await _context.Informacions
                .FirstOrDefaultAsync(i => i.IdEstudiante == idEstudiante);

            if (info == null)
            {
                return View(new Informacion { IdEstudiante = idEstudiante.Value });
            }

            return View(info);
        }

        // POST: Informacions/Actualizar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Actualizar([Bind("IdInformacion,CorreoPersonal,Edad,Telefono,Habilidades,Deficiencias,TiempoDedicacion,ContraseñaNueva")] Informacion info)
        {
            int? idEstudiante = HttpContext.Session.GetInt32("IdEstudiante");

            if (idEstudiante == null)
            {
                return RedirectToAction("Index", "Login");
            }

            info.IdEstudiante = idEstudiante.Value;

            if (!ModelState.IsValid)
            {
                return View(info);
            }

            var existente = await _context.Informacions
                .FirstOrDefaultAsync(i => i.IdEstudiante == info.IdEstudiante);

            if (existente == null)
            {
                _context.Informacions.Add(info);
            }
            else
            {
                existente.CorreoPersonal = info.CorreoPersonal;
                existente.Edad = info.Edad;
                existente.Telefono = info.Telefono;
                existente.Habilidades = info.Habilidades;
                existente.Deficiencias = info.Deficiencias;
                existente.TiempoDedicacion = info.TiempoDedicacion;
                existente.ContraseñaNueva = info.ContraseñaNueva;

                _context.Update(existente);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
