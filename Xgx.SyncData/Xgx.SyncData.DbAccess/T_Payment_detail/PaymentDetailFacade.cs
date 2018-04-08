using Xgx.SyncData.DbAccess.t_unit;
using Xgx.SyncData.DbEntity;

namespace Xgx.SyncData.DbAccess
{
    public class PaymentDetailFacade
    {
        private readonly PaymentDetailCMD _cmd = new PaymentDetailCMD();
        private readonly PaymentDetailQuery _query = new PaymentDetailQuery();

        public dynamic GetPaymentById(string id)
        {
            return _query.GetPaymentById(id);
        }
    }
}
