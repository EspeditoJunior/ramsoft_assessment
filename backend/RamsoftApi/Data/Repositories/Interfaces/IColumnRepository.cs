using Models.Entities;
using RamsoftApi.Models.Requests;

namespace RamsoftApi.Data.Repositories.Interfaces
{
    public interface IColumnRepository
    {
        Column GetById(string id);
        Column Add(ColumnRequest request);
        List<Column> ListByDashBoard(string dashboardId);
        void Delete(string id);
    }
}
