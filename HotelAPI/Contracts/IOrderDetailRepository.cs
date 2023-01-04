using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IOrderDetailRepository
    {
        //查詢所有OrderDetail資料
        public Task<IEnumerable<OrderDetail>> GetOrderDetails();

        //查詢指定OrderID的OrderDetail資料
        public Task<IEnumerable<OrderDetail>> GetSingleOrderDetails(String oid);

        //批次新增OrderDetail資料
        public Task CreateMutipleOrderDetails(List<OrderDetail> orderDetails);

        //批次修改指定ID的OrderDetail資料
        public Task UpdateMutipleOrderDetails(String oid, List<OrderDetail> orderDetails);

        //刪除指定ID的OrderDetail資料
        public Task DeleteMutipleOrderDetails(String oid);
    }
}
