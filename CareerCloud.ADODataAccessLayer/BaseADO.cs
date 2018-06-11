using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.ADODataAccessLayer
{
    public abstract class BaseADO
    {
        protected readonly string ConnectionStr;
        public BaseADO()
        {
            ConnectionStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        }
    }
}
