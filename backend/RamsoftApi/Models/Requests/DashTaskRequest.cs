using Models.Entities;
using System.Threading.Tasks;

namespace RamsoftApi.Models.Requests
{
    public class DashTaskRequest
    {
        public Guid ColumnId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
