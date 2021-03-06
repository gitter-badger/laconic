using System.Collections.Generic;

namespace Laconic
{
    public class GridViewList : ViewList
    {
        internal (int Row, int Column, int RowSpan, int ColumnSpan) GetPositioning(Key key) => _positioning[key];

        internal void SetPositioning(Key key, int row, int column, int rowSpan, int columnSpan) =>
            _positioning[key] = (row, column, rowSpan, columnSpan);

        readonly Dictionary<Key, (int Row, int Column, int RowSpan, int ColumnSpan)> _positioning =
            new Dictionary<Key, (int Row, int Column, int RowSpan, int ColumnSpan)>();

        public static implicit operator GridViewList(Dictionary<(Key Key, int Row, int Column), View> source)
        {
            var res = new GridViewList();
            foreach (var item in source)
            {
                res.Add(item.Key.Key, item.Value);
                res.SetPositioning(item.Key.Key, item.Key.Row, item.Key.Column, 0, 0);
            }

            return res;
        }
    }

    public class ItemsViewList : ViewList
    {
        internal readonly Dictionary<Key, string> ReuseKeys = new Dictionary<Key, string>();

        public View this[string reuseKey, Key key]
        {
            set
            {
                base[key] = value;
                ReuseKeys[key] = reuseKey;
            }
        }

        public void Add(string reuseKey, Key key, View view)
        {
            Add(key, view);
            ReuseKeys[key] = reuseKey;
        }
    }
}