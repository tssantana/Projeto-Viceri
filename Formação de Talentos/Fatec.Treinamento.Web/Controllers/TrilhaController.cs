using Fatec.Treinamento.Data.Repositories;
using Fatec.Treinamento.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fatec.Treinamento.Web.Models;

namespace Fatec.Treinamento.Web.Controllers
{
    // Obs: Colocando o atributo [Authorize] no controller, garante que todas as actions so podem ser
    // acessadas quando o usuário estiver autorizado a utilizar.
    // Quando informo o parametro "Roles", indico que só quem tiver o perfil administrador poderá acessar.
    public class TrilhaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var repo = new TrilhaRepository();

            var lista = repo.Listar();

            return View(lista);
        }

        [HttpGet]
        public ActionResult VincularCursosTrilha(int id)
        {
            var modelView = new VincularCursosTrilhaViewModel();

            // Obtenho a Trilha
            using (var repo = new TrilhaRepository())
            {
                modelView.Trilha = repo.Obter(id);
            }

            // Lista com todos os cursos
            using (var repo = new CursoRepository())
            {
                var lista = repo.ListarCursosDetalhes();

                // transforma os itens da lista nos itens para vincular com a tela:
                // Obs: Esse foreach é desnecessário. Poderia ser resolvido com 1 única linha de lambda, porém
                // usei o for para ser mais fácil de entender.
                foreach (var curso in lista)
                {
                    var item = new SelectListItem
                    {
                        Text = curso.Nome,
                        Value = curso.Id.ToString()
                    };
                    modelView.CursosDisponiveis.Add(item);
                }
            }
            return View(modelView);
        }

        [HttpGet]
        public ActionResult Criar(Trilha Trilha)
        {

            using (var repo = new TrilhaRepository())
            {
                repo.Inserir(Trilha);
            };

            return View();
        }

        public ActionResult Editar( int id)
        {
            var repo = new TrilhaRepository();
            
            Trilha trilha = repo.Obter(id);
            
            return View(trilha);
        }
    }
}