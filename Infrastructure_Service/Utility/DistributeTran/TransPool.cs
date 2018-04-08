using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.Utility.DistributeTran
{
    public class TransPool
    {

        private Dictionary<string, TranInfo> pool;

        public void Regist(string key, TerminalInfo info)
        {
            if (pool[key] == null)
                pool[key] = new TranInfo() { Terminals = new List<TerminalInfo> { } };
            pool[key].Terminals.Add(info);
        }

        public void Remove(string key)
        {
            pool.Remove(key);
        }

        public bool IsTranCommit(string key)
        {
            return pool[key].Terminals.Aggregate(true, (i, j) =>
                {
                    return i && j.IsSubmit;
                });
        }

    }
}
