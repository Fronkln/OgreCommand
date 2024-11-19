using OgreCommandLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OgreCommander
{
    public class TreeNodeCommandset : TreeNode
    {
        public CFCTable Table = null;


        public TreeNodeCommandset()
        {

        }

        public TreeNodeCommandset(CFCTable table)
        {
            Table = table;

            if (string.IsNullOrEmpty(table.Name))
                Text = "Command";
            else
                Text = table.Name;
        }

        public override object Clone()
        {
            TreeNodeCommandset cloned = (TreeNodeCommandset)base.Clone();
            cloned.Table = Table.Copy();

            return cloned;
        }
    }
}
