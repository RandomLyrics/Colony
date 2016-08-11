using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorSQL.Core
{
    public static class QueryExtensions
    {
        public static char _separator = ',';

        public static Query Select(this Query q)
        {
            return q;
        }
        public static Query Columns(this Query q, string cols)
        {
            var x = cols.Split(_separator);
            foreach (var col in x)
            {
                q._columns.Add(col.Trim());
            }
            return q;
        }
        public static Query From(this Query q, string from)
        {
            q._from = from;
            return q;
        }
        public static Query Join(this Query q, string joins)
        {

            var x = joins.Split(_separator);
            foreach (var col in x)
            {
                q._joins.Add(col.Trim());
            }
            return q;
        }
        public static Query Where(this Query q, string wheres)
        {
            q._conditions.Add(wheres);
            //var x = wheres.Split(_separator);
            //foreach (var col in x)
            //{
            //    q._joins.Add(col.Trim());
            //}
            return q;
        }

    }

    public class Query
    {
        internal StringBuilder _quary = new StringBuilder();

        internal ICollection<string> _columns = new Collection<string>();
        internal string _from;
        internal ICollection<string> _joins = new Collection<string>();
        internal ICollection<string> _conditions = new Collection<string>();

        public string Build()
        {
            _quary.Clear();
            _quary.Append("SELECT ");
            foreach (var col in _columns)
            {
                _quary.Append(col + ',');
            }
            --_quary.Length;

            _quary.Append(" FROM ");
            _quary.Append(_from + ' ');
            foreach (var join in _joins)
            {
                _quary.Append(join + ' ');
            }
            foreach (var condi in _conditions)
            {
                _quary.Append(condi + ' ');
            }
            return _quary.ToString();
        }

    }
}
