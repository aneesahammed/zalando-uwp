using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZalandoAPIDemo.Models;

namespace ZalandoAPIDemo.Services
{
    public interface IDataService
    {
        Task<RootObject> GetData(string query, bool isResetRequested = false);
    }
}
