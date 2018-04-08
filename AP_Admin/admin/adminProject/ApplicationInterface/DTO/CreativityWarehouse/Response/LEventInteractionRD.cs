using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class LEventInteractionRD : IAPIResponseData
    {
        public int TotalCount { get; set; }
        public int TotalPageCount { get; set; }
        public IList<T_CTW_LEventInteractionData> LEventInteractionList { get; set; }
        public T_CTW_LEventInteractionData LEventInteractionData { get; set; }
    }
}