using System;
using System.Collections.Generic;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.Tree
{
    public class JsTreeNode
    {
        public string id { set; get; } // نام این خواص باید با مستندات هماهنگ باشد       
        public string text { get; set; }
        public string icon { get; set; }
        public JsTreeNodeState state { set; get; }
        public List<JsTreeNode> children { set; get; }
        public JsTreeNodeLiAttributes li_attr { set; get; }
        public JsTreeNodeAttributes a_attr { set; get; }
        public string GeoType { get; set; }

        public JsTreeNode()
        {
            state = new JsTreeNodeState();
            children = new List<JsTreeNode>();
            li_attr = new JsTreeNodeLiAttributes();
            a_attr = new JsTreeNodeAttributes();
        }
    }
}
