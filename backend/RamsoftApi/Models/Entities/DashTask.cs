namespace Models.Entities
{
    public class DashTask
    {
        public Guid TaskId { get; set; }
        public Guid ColumnId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
