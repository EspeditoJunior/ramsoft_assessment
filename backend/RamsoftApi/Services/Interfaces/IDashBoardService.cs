using Models.Entities;
using RamsoftApi.Models.Requests;

namespace Services.Interfaces
{
    public interface IDashBoardService
    {
        DashBoard Get(string id);
        List<DashBoard> List();
        DashBoard Add(DashBoardRequest request);
        void Delete(string id);

    }
}
