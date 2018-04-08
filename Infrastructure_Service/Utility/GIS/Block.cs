using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.GIS
{
    /// <summary>
    /// 区块信息
    /// </summary>
    public class Block
    {
        /// <summary>
        /// 标签大小
        /// </summary>
        public int LabelSize { get; set; }
        /// <summary>
        /// 区域颜色
        /// </summary>
        public int RegionColor { get; set; }
        /// <summary>
        /// 标签边框颜色
        /// </summary>
        public string LabelBorderColor { get; set; }
        /// <summary>
        /// 区域透明度
        /// </summary>
        public double RegionAlpha { get; set; }
        /// <summary>
        /// 标签背景色
        /// </summary>
        public string LabelBackgroundColor { get; set; }
        /// <summary>
        /// 标签内容
        /// </summary>
        public string LabelContent { get; set; }
        /// <summary>
        /// 标签字体颜色
        /// </summary>
        public int LabelColor { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public string RegionID { get; set; }
        /// <summary>
        /// 标签透明度
        /// </summary>
        public int LabelAlpha { get; set; }
        /// <summary>
        /// 点信息，格式：“x坐标，y坐标；x坐标，y坐标”
        /// </summary>
        public string Points { get; set; }
        
        /// <summary>
        /// 点组成的多边形
        /// </summary>
        public Polygon Polygon
        {
            get
            {
                Polygon Polygon = new Polygon();
                foreach (var item in Points.Split(';'))
                {
                    var s = item.Split(',');
                    if (s.Length == 2)
                    {
                        Polygon.Add(new Coordinate() { Lon = double.Parse(s[0]), Lat = double.Parse(s[1]) });
                    }
                }
                return Polygon;
            }
        }

        /// <summary>
        /// 判断点是否在区域内
        /// </summary>
        /// <param name="pt">点信息</param>
        /// <returns></returns>
        public bool IsExits(Coordinate pt)
        {
            return MapMethods.PointInPolygon(pt, this.Polygon);
        }
    }
}
