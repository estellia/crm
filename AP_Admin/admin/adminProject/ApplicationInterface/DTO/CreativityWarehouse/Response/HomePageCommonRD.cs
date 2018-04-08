using adminProject.ApplicationInterface.DTO.Base;
using cPos.Admin.Model.CreativityWarehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Response
{
    public class HomePageCommonRD : IAPIResponseData
    {
        public T_CTW_HomePageCommonData HomePageCommon { get; set; }

        public string ImageUrl { get; set; }
    }
}