using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public  class Column
    {
        public Guid ColumnId { get; set; }
        public string Name { get; set; }
        public List<DashTask> Tasks { get; set; }
    }
}
