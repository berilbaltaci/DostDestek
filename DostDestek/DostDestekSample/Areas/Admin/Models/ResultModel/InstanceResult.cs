using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DostDestek.Common;           

namespace DostDestekSample.Areas.Admin.Models.ResultModel
{
    public class InstanceResult<T>
    {
        public Result<List<T>> resultList { get; set; }
        public Result<int> resultint { get; set; }
        public Result<T> TResult { get; set; }
    }
}