using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XamTime.Services
{
    public interface ICookieManager
    {
        CookieContainer InitCookieContainer();

        void Clear();
    }
}
