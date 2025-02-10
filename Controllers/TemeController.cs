using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyApp.Models;
using MyApp.Repositories;
using MyApp.ViewModels;

namespace MyApp.Controllers
{
    public class TemeController : Controller
    {
        private readonly ITemeRepository _temeRepository;

        public TemeController(ITemeRepository temeRepository)
        {
            _temeRepository = temeRepository;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var temeDisponibile = await _temeRepository.GetTemeDisponibileAsync();
            var temeAlese = await _temeRepository.GetTemeAleseAsync(User.Identity.Name);

            var viewModel = new TemeViewModel
            {
                TemeDisponibile = temeDisponibile,
                TemeAlese = temeAlese
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Create()
        {
            var difficultyLevels = await _temeRepository.GetDificultyLevels();
            ViewBag.DificultyLevels = new SelectList(difficultyLevels, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Titlu, Termen, Descriere, DificultyLevelId")] Item item)
        {
            if (ModelState.IsValid)
            {
                await _temeRepository.AddTemaAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _temeRepository.GetTemaByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var difficultyLevels = await _temeRepository.GetDificultyLevels();
            ViewBag.DificultyLevels = new SelectList(difficultyLevels, "Id", "Name", item.DificultyLevelId);
            return View(item);
        }



        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Titlu, Termen, Descriere, DificultyLevelId")] Item item)
        {
            if (ModelState.IsValid)
            {
                await _temeRepository.UpdateTemaAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [Authorize(Roles = "Profesor")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _temeRepository.GetTemaByIdAsync(id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _temeRepository.DeleteTemaAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AlegereTema(int id)
        {
            var tema = await _temeRepository.GetTemaByIdAsync(id);
            if (tema == null)
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AlegereTema(int id, [Bind("IdTema, UserEmail")] TemaAleasa tema)
        {
            tema.IdTema = id;
            ModelState.ClearValidationState(nameof(TemaAleasa));
            ModelState.Remove(nameof(tema.Tema));

            var exists = await _temeRepository.TemaEsteAleasaAsync(tema.IdTema, tema.UserEmail);
            if (exists)
            {
                ModelState.AddModelError(string.Empty, "Ai ales deja această temă.");
                return View(tema);
            }

            if (ModelState.IsValid)
            {
                await _temeRepository.AddTemaAleasaAsync(tema);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
