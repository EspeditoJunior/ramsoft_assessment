using Models.Entities;
using RamsoftApi.Data.Repositories.Interfaces;
using RamsoftApi.Models.Requests;
using RamsoftApi.Services.Interfaces;

namespace RamsoftApi.Services.Services
{
    public class ColumnService : IColumnService
    {
        private readonly IColumnRepository _repository;

        public ColumnService(IColumnRepository repository)
        {
            _repository = repository;
        }

        public Column Add(ColumnRequest request)
        {
            return _repository.Add(request);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public Column Get(string id)
        {
            return _repository.GetById(id);
        }

        public List<Column> ListByDashBoard(string dashBoardId)
        {
            return _repository.ListByDashBoard(dashBoardId);
        }
    }
}
