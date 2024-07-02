using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Domain.Constans
{
    //internal class StockCheckConstant
    //{
    //}

    public static class TrackingStockCheckTitleDict
    {
        public static string GetTitle(StockCheckStatus status, string userName)
        {
            switch (status)
            {
                case StockCheckStatus.Todo:
                    return string.Format("Đợt kiểm kho đã được tạo bởi quản lý {0}", userName);
                case StockCheckStatus.Assigned:
                    return string.Format("Đợt kiểm kho đã được giao cho nhân viên {0}", userName);
                case StockCheckStatus.Accepted:
                    return string.Format("Nhân viên {0} đã chấp nhận đợt kiểm kho", userName);
                case StockCheckStatus.AssignmentDeclined:
                    return string.Format("Nhân viên {0} đã từ chối đợt kiểm kho", userName);
                case StockCheckStatus.Draft:
                    return string.Format("Nhân viên {0} đã cập nhật đợt kiểm kho", userName);
                case StockCheckStatus.Submitted:
                    return string.Format("Nhân viên {0} đã hoàn thành đợt kiểm kho", userName);
                case StockCheckStatus.Confirmed:
                    return string.Format("Thủ kho {0} đã xác nhận cho đợt kiểm kho", userName);
                case StockCheckStatus.Completed:
                    return string.Format("Đợt kiểm kho đã hoàn thành bởi quản lý {0}", userName);
                case StockCheckStatus.Rejected:
                    return string.Format("Đợt kiểm kho đã bị từ chối bởi {0}", userName);
                default:
                    throw new Exception("TrackingStockCheckTitleDict - Invalid stock check status");
            }
        }

    }
}
