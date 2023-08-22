using Models.Entities;
using RamsoftApi.Data.Repositories.Interfaces;
using RamsoftApi.Models.Requests;
using System.Data;
using RamsoftApi.Data.Helpers;

namespace RamsoftApi.Data.Repositories
{
    public class DashBoardRepository : IDashBoardRepository
    {
        public DashBoard Add(DashBoardRequest request)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();

            if (items.Any(i => i.Name == request.Name))
            {
                throw new DuplicateNameException();
            }

            var newDashBoard = new DashBoard();
            newDashBoard.Name = request.Name;
            newDashBoard.DashBoardId = Guid.NewGuid();
            newDashBoard.Columns = new List<Column>();



            items.Add(newDashBoard);
            DataBaseHelper.SaveNewEntries(items);
            return newDashBoard;
        }

        public DashBoard GetById(string id)
        {
            return DataBaseHelper.GetAllDashBoardEntries().First(d => d.DashBoardId == Guid.Parse(id));
        }

        public List<DashBoard> List()
        {
            return DataBaseHelper.GetAllDashBoardEntries();
        }


        public void Delete(string id)
        {
            var items = DataBaseHelper.GetAllDashBoardEntries();
            items.RemoveAll(x => x.DashBoardId == Guid.Parse(id));
            DataBaseHelper.SaveNewEntries(items);
        }
    }
}
