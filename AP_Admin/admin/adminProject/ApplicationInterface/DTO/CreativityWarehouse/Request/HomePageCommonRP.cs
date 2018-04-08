using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class HomePageCommonRP : IAPIRequestParameter
    {
        public string AdId { get; set; }
        public string ImageId { get; set; }

        public void Validate()
        { }
    }
}