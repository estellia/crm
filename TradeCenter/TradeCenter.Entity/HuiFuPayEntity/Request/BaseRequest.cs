using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.TradeCenter.Entity.HuiFuPayEntity.Request
{
    public class BaseRequest<T>
    {
        public string bizCd = "";
        // public string dataCount = "";
        public T reqBody;
        public string reqSeq = "";
        // public int reqTms;
    }


}
