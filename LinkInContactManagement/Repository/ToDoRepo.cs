using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkInContactManagement.Model;

namespace LinkInContactManagement.Repository
{
    public class ToDoRepo
    {
        private ContactContext context = new ContactContext();

        public List<ToDo> GetToDoList()
        {
            return context.ToDoList.ToList();
        }

        public void AddNewToDo(ToDo newToDo)
        {
            context.ToDoList.Add(newToDo);
            context.SaveChanges();
        }

        public void DeleteToDoList(List<ToDo> toDoList)
        {
            foreach (var todo in toDoList)
            {
                var dbTD = context.ToDoList.FirstOrDefault(t => t.ToDoId == todo.ToDoId);
                if (dbTD != null)
                {
                    context.ToDoList.Remove(dbTD);
                }
            }

            context.SaveChanges();
        }

        public void UpdateToDo(ToDo toDoSource)
        {
            var dbTd = context.ToDoList.FirstOrDefault(t => t.ToDoId == toDoSource.ToDoId);

            if (dbTd != null)
            {
                dbTd.DateCompleted = toDoSource.DateCompleted;
                dbTd.Description = toDoSource.Description;
                dbTd.DueDate = toDoSource.DueDate;
                dbTd.Title = toDoSource.Title;
                context.SaveChanges();
            }
        }
    }
}
