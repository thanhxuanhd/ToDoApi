using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp
{
    public class TodoDbInitializer
    {
        public TodoDbInitializer()
        {
        }

        public static void Initializer(TodoDbContext context)
        {
            if (!context.TodoItems.Any())
            {
                var todoItems = new List<TodoItem>();
                for (int i = 0; i < 10; i++)
                {
                    var todoItem = new TodoItem
                    {
                        Name = $"Todo Application {i + 1}",
                        IsComplete = false
                    };
                    todoItems.Add(todoItem);
                };
                context.AddRange(todoItems);
                context.SaveChanges();
            }
        }

    }
}
