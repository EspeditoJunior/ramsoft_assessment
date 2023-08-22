using Models.Entities;
using RamsoftApi.Models.Requests;

namespace RamsoftApi.Services.Interfaces
{
    public interface IColumnService
    {
        Column Get(string id);
        List<Column> ListByDashBoard(string dashBoardId);
        Column Add(ColumnRequest request);
        void Delete(string id);
    }
}
