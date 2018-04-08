using adminProject.ApplicationInterface.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace adminProject.ApplicationInterface.DTO.CreativityWarehouse.Request
{
    public class ObjectImagesRP:IAPIRequestParameter
    {
        public string ImageId { get; set; }
        public string ImageURL { get; set; }
        public string BatId { get; set; }
        /// <summary>
        /// 是：1 否：0
        /// </summary>
        public int IsHomePage { get; set; }
        //计划图片关联表ID
        public string AdId { get; set; }
        public void Validate()
        { }
    }
}