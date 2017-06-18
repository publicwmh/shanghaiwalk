using Microsoft.Extensions.Logging;
using shanghaiwalk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shanghaiwalk.Baiye
{
    public class QueryHisService
    {
        private BaiYeContext _baiyecontext;
        private readonly ILogger _logger;

        public QueryHisService(
            BaiYeContext baiyecontent,
            ILogger logger)
        {

            _baiyecontext = baiyecontent;
            _logger = logger;
        }

        public void SaveQueryHis(string openid, string querytxy)
        {
            QueryHis his = new QueryHis() { createtime = DateTime.Now, entertxt = querytxy, openid = openid };
            _baiyecontext.QueryHiss.Add(his);
            _baiyecontext.SaveChanges();
        }
    }
}
