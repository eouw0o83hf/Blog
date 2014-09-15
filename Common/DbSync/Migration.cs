using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbSync
{
    public interface IMigration
    {
        long Version { get; }
        string SqlCommand { get; }
    }
}
