using Site_Lanchonete.Models;
using Site_Lanchonete.Repositories.Interfaces;
using Site_Lanchonete.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Site_Lanchonete.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(l => l.Categoria.CategoriaNome == categoria)
                    .OrderBy(c => c.Nome);
                categoriaAtual = categoria;
            }
            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            };
            return View(lanchesListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
            return View(lanche);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(p => p.LancheId);
                categoriaAtual = "Todos os Lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel 
            { 
                Lanches = lanches, 
                CategoriaAtual= categoriaAtual 
            });
        }
    }
}
