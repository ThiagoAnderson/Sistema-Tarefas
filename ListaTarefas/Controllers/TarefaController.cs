using ClosedXML.Excel;
using ListaTarefas.Data;
using ListaTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

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
        [HttpGet]

        public IActionResult Exportar()
        {
            var dados = GetDados();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados , "Dados Tarefas");

                using (MemoryStream ms = new MemoryStream()) 
                {
                    workbook.SaveAs(ms); 
                    return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spredsheetml.sheet","Tarefa.xls");
                }
            }
        }

        private DataTable GetDados()
        {
            DataTable datatable = new DataTable();

            datatable.TableName = "Dados Tarefa";
            datatable.Columns.Add("Titulo Tarefa", typeof(string));
            datatable.Columns.Add("Descriçao Tarefa", typeof(string));
            datatable.Columns.Add("Data criação Tarefa", typeof(DateTime));

            var dados = _bancoContext.Tarefa.ToList();

            if (dados.Count > 0)
            {
                dados.ForEach(tarefa =>
                {
                    datatable.Rows.Add(tarefa.Nome,tarefa.Descricao,tarefa.DataCriacao);
                });
            }

            return datatable;

        }

        [HttpPost]
        public IActionResult Criar(TarefaModel tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.DataCriacao = DateTime.Now;

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
                var tarefaDB = _bancoContext.Tarefa.Find(tarefa.Id);

                tarefaDB.Nome = tarefa.Nome;
                tarefaDB.Descricao = tarefa.Descricao;

                _bancoContext.Tarefa.Update(tarefaDB);
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
