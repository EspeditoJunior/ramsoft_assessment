namespace Models.Entities
{
    public class DashBoard
    {
        public Guid DashBoardId { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; }
    }
}
