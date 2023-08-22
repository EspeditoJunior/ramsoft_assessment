using Models.Entities;
using RamsoftApi.Data.Repositories.Interfaces;
using RamsoftApi.Models.Requests;
using RamsoftApi.Models.Responses;
using RamsoftApi.Services.Interfaces;

namespace RamsoftApi.Services.Services
{
    public class TaskService : ITaskService
    {

        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public AddTaskResponse Add(DashTaskRequest request)
        {
            return _repository.Add(request);
        }

        public void Delete(string id)
        {
            _repository.Delete(id);
        }

        public AddTaskResponse Get(string id)
        {
            return _repository.GetById(id);
        }

        public AddTaskResponse Update(DashTask request)
        {
            return _repository.Update(request);
        }
    }
}
