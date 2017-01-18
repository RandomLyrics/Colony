using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorSQL.Core
{
    public class SqlChannel : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _con.Close();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }
        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~SqlChannel() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        private SqlConnection _con;
        private SqlCommand _command;
        private SqlDataReader _reader;

        public SqlChannel(string connectionString)
        {
            _con = new SqlConnection(connectionString);
            _con.Open();
        }
        
        public SqlDataReader Send(string quary)
        {
            _command = new SqlCommand(quary, _con);
            return _command.ExecuteReader();
        }

        public List<T> GetRecords<T>(Query qr) where T: class, new()
        {
            qr.Command.Connection = _con;
            var reader = qr.Command.ExecuteReader();
            return Mapper.FromReader<T>(reader);
        }
    }
}
