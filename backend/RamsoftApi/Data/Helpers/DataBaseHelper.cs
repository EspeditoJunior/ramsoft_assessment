using Models.Entities;
using Newtonsoft.Json;

namespace RamsoftApi.Data.Helpers
{
    public static class DataBaseHelper
    {
        public static List<DashBoard> GetAllDashBoardEntries()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataBaseFiles", "DashBoards.json");

            StreamReader r = new StreamReader(path);

            string json = r.ReadToEnd();
            var items = JsonConvert.DeserializeObject<List<DashBoard>>(json);
            r.Close();
            return items;
        }

        public static List<DashBoard> SaveNewEntries(List<DashBoard> newContent)
        {

            var json = JsonConvert.SerializeObject(newContent);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "DataBaseFiles", "DashBoards.json");
            File.WriteAllText(path, json);

            return GetAllDashBoardEntries();
        }
    }
}
