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
    }
}
