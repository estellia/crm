using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cPos.Admin.Component;
using cPos.Admin.Component.SqlMappers;
using cPos.Admin.Model.Customer;
using cPos.Model;

namespace cPos.Admin.Service.Interfaces
{
    public interface IUnitService
    {
        IList<UnitInfo> GetUnitInfoList(Hashtable condition, int maxRowCount, int startRowIndex);
        int GetUnitInfoListCount(Hashtable condition);
    }
}
