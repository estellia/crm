using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel.Activation;
using System.ServiceModel;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Xml;
using cPos.Dex.Model;
using cPos.Dex.Common;
using System.Data;
using System.Collections;
using cPos.Dex.Services;
using cPos.Dex.ContractModel;

namespace cPos.Dex.WcfService
{
    /// <summary>
    /// TestService
    /// </summary>
    public class TestService : BaseService, ITestService
    {
        #region TestConnect
        /// <summary>
        /// 连接测试
        /// </summary>
        public BaseContract TestConnect(string userId)
        {
            var data = new BaseContract();
            data.status = "true";
            return data;
        }
        #endregion
    }
}