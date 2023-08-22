using Models.Entities;
using RamsoftApi.Models.Requests;

namespace RamsoftApi.Data.Repositories.Interfaces
{
    public interface IDashBoardRepository
    {
        DashBoard GetById(string id);
        DashBoard Add(DashBoardRequest request);
        List<DashBoard> List();
        void Delete(string id);
    }
}
