using Models.Entities;
using RamsoftApi.Models.Requests;
using RamsoftApi.Models.Responses;

namespace RamsoftApi.Services.Interfaces
{
    public interface ITaskService
    {
        AddTaskResponse Get(string id);
        AddTaskResponse Add(DashTaskRequest request);
        AddTaskResponse Update(DashTask request);
        void Delete(string id);
    }
}
