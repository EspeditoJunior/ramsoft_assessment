using Models.Entities;
using RamsoftApi.Models.Requests;
using RamsoftApi.Models.Responses;

namespace RamsoftApi.Data.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        AddTaskResponse GetById(string id);
        AddTaskResponse Add(DashTaskRequest request);
        AddTaskResponse Update(DashTask request);
        void Delete(string id);
    }
}
