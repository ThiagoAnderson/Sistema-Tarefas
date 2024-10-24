using ListaTarefas.Data;
using ListaTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaTarefas.Controllers
{
    public class TarefaController : Controller
    {
        readonly private BancoContext _bancoContext;
        public TarefaController(BancoContext db)
        {
            _bancoContext = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TarefaModel> tarefas = _bancoContext.Tarefa;
            return View(tarefas);
        }
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }

            TarefaModel tarefa = _bancoContext.Tarefa.FirstOrDefault(x => x.Id == id);

            if(tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }
        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            TarefaModel tarefa = _bancoContext.Tarefa.FirstOrDefault(x => x.Id == id);

            if(tarefa == null)
            {
                return NotFound();
            }
            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(TarefaModel tarefa)
        {
            if (ModelState.IsValid)
            {
                _bancoContext.Tarefa.Add(tarefa);
                _bancoContext.SaveChanges();
                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso";

                return RedirectToAction("Index");
            }

            return View();
        }
        [HttpPost]
        public IActionResult Editar(TarefaModel tarefa)
        {
            if (ModelState.IsValid)
            {
                _bancoContext.Tarefa.Update(tarefa);
                _bancoContext.SaveChanges();
                TempData["MensagemEdicao"] = "Edição realizado com sucesso";
                return RedirectToAction("Index");
            }

            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Excluir(TarefaModel tarefa)
        {
            if(tarefa == null)
            {
                return NotFound();
            }
            _bancoContext.Tarefa.Remove(tarefa);
            _bancoContext.SaveChanges();
            TempData["MensagemExclusao"] = "Exclusão realizado com sucesso";

            return RedirectToAction("Index");

        }
    }
}
