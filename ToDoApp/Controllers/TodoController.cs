using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private TodoDbContext _todoDbContext;

        public TodoController(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        [HttpGet]
        public IActionResult Get(int page = 0, int pageSize = 15)
        {
            var todoItems = _todoDbContext.TodoItems
                .Skip(page * pageSize)
                .Take(pageSize).ToList();
            return Json(todoItems);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TodoItem model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => new { key = x.Key, value = x.Value }).ToList();
                return BadRequest(errors);
            }

            _todoDbContext.TodoItems.Add(model);
            _todoDbContext.SaveChanges();
            return Json(model.Id); ;
        }

        [HttpPut]
        public IActionResult Put([FromBody] TodoItem model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => new { key = x.Key, value = x.Value }).ToList();
                return BadRequest(errors);
            }

            var todoEntity = _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == model.Id);
            if (todoEntity == null)
            {
                return BadRequest(new { key = "Id", value = "Item not exists" });
            }

            todoEntity.IsComplete = model.IsComplete;
            todoEntity.Name = model.Name;
            _todoDbContext.SaveChanges();
            return Json(model.Id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => new { key = x.Key, value = x.Value }).ToList();
                return BadRequest(errors);
            }

            var todoEntity = _todoDbContext.TodoItems.FirstOrDefault(x => x.Id == id);
            if (todoEntity == null)
            {
                return BadRequest(new { key = "Id", value = "Item not exists" });
            }

            _todoDbContext.SaveChanges();
            return Json(id);
        }
    }
}