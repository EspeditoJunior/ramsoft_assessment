using Models.Entities;
using RamsoftApi.Data.Repositories.Interfaces;
using RamsoftApi.Models.Requests;
using Services.Interfaces;

namespace Services.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IDashBoardRepository _repository;

        public DashBoardService(IDashBoardRepository repository)
        {
            _repository = repository;
        }

        public DashBoard Add(DashBoardRequest request)
        {
            return _repository.Add(request);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public DashBoard Get(string id)
        {
            return _repository.GetById(id);
        }

        public List<DashBoard> List()
        {
            return _repository.List();
        }

    }
}
