using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCMAPI.DBAccess
{
    /// <summary>
    /// Class to return single result set
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class SingleResultSet<T1>
    {
        public IList<T1> ResultSet { get; set; }
    }
}
