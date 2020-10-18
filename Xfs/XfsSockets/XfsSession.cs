using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Xfs
{
    public class XfsSession : XfsComponent
    {
        public bool IsLogin { get; set; } = false;
        public int bookersChange { get; set; } = -1;
        public int teachersChange { get; set; } = -1;
        public int engineersChange { get; set; } = -1;
        public int inventorysChange { get; set; } = -1;
        public int skillsChange { get; set; } = -1; 
    }
}