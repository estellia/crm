using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.GIS
{
    /// <summary>
    /// 多边形
    /// </summary>
    public class Polygon
    {
        public Polygon()
        {
            _coordinatelist = new List<Coordinate> { };
        }

        List<Coordinate> _coordinatelist;

        public void Add(Coordinate pt)
        {
            this._coordinatelist.Add(pt);
        }

        public int Count { get { return this._coordinatelist.Count; } }

        public Coordinate this[int index]
        {
            get { return this._coordinatelist[index]; }
        }
        /// <summary>
        /// 删除最近加入的一个点
        /// </summary>
        public void Remove()
        {
            if (this._coordinatelist.Count == 0)
                throw new Exception("已经没有坐标点可以删除");
            this._coordinatelist.RemoveAt(_coordinatelist.Count - 1);
        }

        /// <summary>
        /// 判断是否是一个多边形
        /// </summary>
        /// <returns></returns>
        public bool IsMatch()
        {
            return this.Count >= 3 && !(this._coordinatelist.FindAll(t => t.Lat == this[0].Lat).Count == this.Count) && !(this._coordinatelist.FindAll(t => t.Lon == this[0].Lon).Count == this.Count);
        }
    }
}
