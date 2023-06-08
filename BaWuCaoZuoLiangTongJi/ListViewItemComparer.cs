using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaWuCaoZuoLiangTongJi
{
    public class ListViewItemComparer : IComparer
    {
        public ListViewItemComparer()
        {
            SortColumn = 0;
            Order = SortOrder.None;
        }

        public int SortColumn { get; set; }

        public SortOrder Order { get; set; }

        public ListViewItemComparer(int column) : this()
        {
            SortColumn = column;
        }

        public ListViewItemComparer(int column, SortOrder sortOrder) : this(column)
        {
            Order = sortOrder;
        }

        public int Compare(object x, object y)
        {
            int.TryParse(((ListViewItem)x).SubItems[SortColumn].Text, out int a);
            int.TryParse(((ListViewItem)y).SubItems[SortColumn].Text, out int b);

            int result = a.CompareTo(b);

            if (Order == SortOrder.Ascending)
            {
                return result;
            }
            else if (Order == SortOrder.Descending)
            {
                return (-result);
            }
            else
            {
                return 0;
            }
        }
    }
}
