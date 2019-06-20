using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.Tree
{
    public class TreeViewProperties
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int? ParentId { get; set; }
    }
}
