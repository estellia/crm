/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/10 9:56:47
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.MSTRIntegration.Base;
using JIT.Utility.MSTRIntegration.Entity;

namespace JIT.Utility.MSTRIntegration.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ� MSTR����Ŀ,�������Ϣ��Ҫ���ڴ���MSTR��Session��ͬʱ��Ŀ����ϢҲ�����ڼ���ʱ���ɷ���MSTR�����Url 
    /// ��MSTRProject�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class MSTRProjectDAO : BaseReportDAO, ICRUDable<MSTRProjectEntity>, IQueryable<MSTRProjectEntity>
    {
        
    }
}
