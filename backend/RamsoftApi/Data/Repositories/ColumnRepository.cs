using Models.Entities;
using RamsoftApi.Data.Helpers;
using RamsoftApi.Data.Repositories.Interfaces;
using RamsoftApi.Models.Requests;

namespace RamsoftApi.Data.Repositories
{
    public class ColumnRepository : IColumnRepository
    {
        public Column Add(ColumnRequest request)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();
            var dash = items.First(x => x.DashBoardId == request.DashBoardId);
            items.Remove(dash);

            var newColumn = new Column();
            newColumn.ColumnId = Guid.NewGuid();
            newColumn.Name = request.Name;
            newColumn.Tasks = new List<DashTask>();

            if (dash.Columns == null)
            {
                dash.Columns = new List<Column>();
            }

            dash.Columns.Add(newColumn);
            items.Add(dash);
            DataBaseHelper.SaveNewEntries(items);

            return newColumn;

        }

        public void Delete(string id)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();
            var dash = items.First(i=>i.Columns.Any(c => c.ColumnId == Guid.Parse(id)));
            items.Remove(dash);
            
            dash.Columns.RemoveAll(x => x.ColumnId == Guid.Parse(id));
            items.Add(dash);
            DataBaseHelper.SaveNewEntries(items);
        }

        public Column GetById(string id)
        {
            throw new NotImplementedException();
        }

        public List<Column> ListByDashBoard(string dashboardId)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries().First(d => d.DashBoardId == Guid.Parse(dashboardId));
            return items.Columns;
        }
    }
}
