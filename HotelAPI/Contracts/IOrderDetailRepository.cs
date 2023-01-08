using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IOrderDetailRepository
    {
        //查詢所有OrderDetail資料
        public Task<IEnumerable<OrderDetail>> GetOrderDetails();

        //查詢指定OrderID的單一OrderDetail資料
        public Task<OrderDetail> GetOrderDetail(String oid);

        //批次新增OrderDetail資料
        public Task CreateMultipleOrderDetails(List<OrderDetailForCreationDto> orderDetails);

        //批次修改指定ID的OrderDetail資料
        //public Task UpdateMultipleOrderDetails(String oid, OrderDetailForUpdateDto orderDetails);

        //刪除指定ID的OrderDetail資料
        public Task DeletOrderDetail(String oid);
    }
}
