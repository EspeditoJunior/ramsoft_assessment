using Models.Entities;

namespace RamsoftApi.Models.Responses
{
    public class AddColumnResponse
    {
        public Guid ColumnId { get; set; }
        public Guid DashBoardId { get; set; }
        public string Name { get; set; }
        public List<DashTask> Tasks { get; set; }
    }
}
