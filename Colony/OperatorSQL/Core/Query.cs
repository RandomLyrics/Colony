using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OperatorSQL.Core
{
    public static class Mapper
    {
        //GET PROPERTIES
        private readonly static IDictionary<Type, IEnumerable<PropertyInfo>> _classProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        internal static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            if (!_classProperties.ContainsKey(type))
                _classProperties[type] = type.GetProperties();
            return _classProperties[type];
        }

        public static IEnumerable<T> FromReader<T>(SqlDataReader reader) where T: class, new()
        {
            var list = new Collection<T>();
            while (reader.Read())
            {
                var obj = new T();
                var props = GetProperties(typeof(T));
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (!reader.IsDBNull(i))
                    {
                        var type = reader.GetFieldType(i);
                        var name = reader.GetName(i);
                        var value = Convert.ChangeType(reader.GetValue(i), type);
                        var prop = props.FirstOrDefault(x => x.Name == name);
                        if (prop != null)
                            prop.SetValue(obj, value);
                    }

                }
                list.Add(obj);
            }
            return list;
        }
    }

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
