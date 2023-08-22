using Models.Entities;
using RamsoftApi.Data.Helpers;
using RamsoftApi.Data.Repositories.Interfaces;
using RamsoftApi.Models.Requests;
using RamsoftApi.Models.Responses;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace RamsoftApi.Data.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public AddTaskResponse Add(DashTaskRequest request)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();
            var dash = items.First(i => i.Columns.Any(c => c.ColumnId == request.ColumnId));
            items.Remove(dash);
           
            var column = dash.Columns.First(c => c.ColumnId == request.ColumnId);

            if (column.Tasks == null)
            {
                column.Tasks = new List<DashTask>();
            }

            if (column.Tasks.Any(t => t.Name == request.Name))
            {
                throw new DuplicateNameException();
            }


            var newTask = new DashTask();
            newTask.TaskId = Guid.NewGuid();
            newTask.ColumnId = column.ColumnId;
            newTask.Name = request.Name;
            newTask.Description = request.Description;

            column.Tasks.Add(newTask);


            items.Add(dash);
            DataBaseHelper.SaveNewEntries(items);

            var response = new AddTaskResponse();
            response.TaskId = Guid.NewGuid();
            response.DashBoardId = dash.DashBoardId;
            response.ColumnId = column.ColumnId;
            response.Name = request.Name;
            response.Description = request.Description;



            return response;

        }

        public void Delete(string id)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();


            var isBreack = false;

            var dash = new DashBoard();
            var column = new Column();
            var task = new DashTask();

            foreach (var dashIterate in items)
            {
                dash = dashIterate;
                foreach (var columnIterate in dashIterate.Columns)
                {
                    column = columnIterate;
                    foreach (var taskIterate in columnIterate.Tasks)
                    {
                        if (taskIterate.TaskId == Guid.Parse(id))
                        {
                            task = taskIterate;
                            isBreack = true;
                        }

                        if (isBreack) { break; }
                    }
                    if (isBreack) { break; }
                }
                if (isBreack) { break; }
            }

            column.Tasks.Remove(task);


            DataBaseHelper.SaveNewEntries(items);
        }

        public AddTaskResponse GetById(string id)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();


            var dash = new DashBoard();
            var column = new Column();
            var task = new DashTask();

            var isBreack = false;

            foreach (var dashIterate in items)
            {
                dash = dashIterate;
                foreach (var columnIterate in dashIterate.Columns)
                {
                    column = columnIterate;
                    foreach (var taskIterate in columnIterate.Tasks)
                    {
                        if (taskIterate.TaskId == Guid.Parse(id))
                        {
                            task = taskIterate;
                            isBreack = true;        
                        } 
                        
                        if(isBreack) { break; }
                    }
                    if (isBreack) { break; }
                }
                if (isBreack) { break; }
            }

            var response = new AddTaskResponse();
            response.TaskId = task.TaskId;
            response.DashBoardId = dash.DashBoardId;
            response.ColumnId = task.ColumnId;
            response.Name = task.Name;
            response.Description = task.Description;

            return response;

        }

        public AddTaskResponse Update(DashTask request)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();
            var dash = items.First(i => i.Columns.Any(c => c.ColumnId == request.ColumnId));
            items.Remove(dash);

            var column = dash.Columns.First(c => c.ColumnId == request.ColumnId);

            if (column.Tasks == null)
            {
                column.Tasks = new List<DashTask>();
            }

            column.Tasks.RemoveAll(t => t.TaskId == request.TaskId);

            if (column.Tasks.Any(t => t.Name == request.Name))
            {
                throw new DuplicateNameException();
            }

            var newTask = new DashTask();
            newTask.TaskId = request.TaskId;
            newTask.ColumnId = column.ColumnId;
            newTask.Name = request.Name;
            newTask.Description = request.Description;



            foreach (var iterateColumns in dash.Columns)
            {
                iterateColumns.Tasks.RemoveAll(t => t.TaskId == request.TaskId);
            }



            column.Tasks.Add(newTask);


            items.Add(dash);
            DataBaseHelper.SaveNewEntries(items);


            var response = new AddTaskResponse();
            response.TaskId = newTask.TaskId;
            response.DashBoardId = dash.DashBoardId;
            response.ColumnId = newTask.ColumnId;
            response.Name = newTask.Name;
            response.Description = newTask.Description;


            return response;
        }
    }
}
