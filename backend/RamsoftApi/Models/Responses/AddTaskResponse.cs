namespace RamsoftApi.Models.Responses
{
    public class AddTaskResponse
    {
        public Guid TaskId { get; set; }
        public Guid ColumnId { get; set; }
        public Guid DashBoardId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
