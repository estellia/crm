using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainClouds.Weixin.Containers;

namespace ChainClouds.Weixin.Cache
{
    //public interface IContainerCacheStragegy : IBaseCacheStrategy
    //{
    //}

    public interface IContainerItemCollection : IDictionary<string, IBaseContainerBag>
    {
    }

    public class ContainerItemCollection : Dictionary<string, IBaseContainerBag>, IContainerItemCollection
    {
    }


    public interface IContainerCacheStragegy : /*IContainerCacheStragegy,*/ IBaseCacheStrategy<string, IContainerItemCollection>
    //where TContainerBag : class, IBaseContainerBag
    {
    }
}
