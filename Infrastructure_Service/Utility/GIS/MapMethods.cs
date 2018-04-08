using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.Utility.ExtensionMethod;

namespace JIT.Utility.GIS
{
    public class MapMethods
    {
        /// <summary>
        /// 判断点是否在几个点构建的多边形面中
        /// </summary>
        /// <param name="p">查询的点</param>
        /// <param name="ptPolygon">构建多边形的点集合</param>
        /// <returns></returns>
        public static bool PointInPolygon(Coordinate p, Polygon ptPolygon)
        {
            if (!ptPolygon.IsMatch())
                throw new Exception("不是一个正确的多边形，请检查点的数量的坐标");
            try
            {
                int nCross = 0;
                int nCount = ptPolygon.Count;

                for (int i = 0; i < nCount; i++)
                {
                    Coordinate p1 = (Coordinate)ptPolygon[i];
                    Coordinate p2 = (Coordinate)ptPolygon[(i + 1) % nCount];

                    // 求解 y=p.y 与 p1p2 的交点 

                    if (p1.Lat == p2.Lat) // p1p2 与 y=p0.y平行 
                        continue;

                    if (p.Lat < Math.Min(p1.Lat, p2.Lat)) // 交点在p1p2延长线上 
                        continue;
                    if (p.Lat >= Math.Max(p1.Lat, p2.Lat)) // 交点在p1p2延长线上 
                        continue;

                    // 求交点的 X 坐标 -------------------------------------------------------------- 
                    double x = (p.Lat - p1.Lat) * (p2.Lon - p1.Lon) / (p2.Lat - p1.Lat) + p1.Lon;

                    if (x > p.Lon)
                        nCross = nCross + 1; // 只统计单边交点 
                }

                // 单边交点为偶数，点在多边形之外 --- 
                return (nCross % 2 == 1);
            }
            catch (Exception ex)
            { }
            return false;
        }

        /// <summary>
        /// 判断点是否在几个点构建的多边形面中
        /// </summary>
        /// <param name="p">查询的点</param>
        /// <param name="block">区块</param>
        /// <returns></returns>
        public static bool PointInPloygon(Coordinate p, Block block)
        {
            return PointInPolygon(p, block.Polygon);
        }

        /// <summary>
        /// 判断点是否在几个点构建的多边形面中
        /// </summary>
        /// <param name="p">查询的点</param>
        /// <param name="blockJson">区块的JSON字符串</param>
        /// <returns></returns>
        public static bool PointInPloygon(Coordinate p, string blockJson)
        {
            try
            {
                var block = blockJson.DeserializeJSONTo<Block>();
                return PointInPloygon(p, block);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
