using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IOrderRepository
    {
        //查詢所有訂單資料
        public Task<IEnumerable<Order>> GetOrders();

        //查詢指定ID的訂單資料
        public Task<Order> GetOrder(String oid);

        //新增Order資料
        public Task<Order> CreateOrder(OrderForCreationDto order);

        //修改指定ID的訂單資料
        public Task<Order> UpdateOrder(String oid, OrderForUpdateDto order);

        //刪除指定ID的訂單資料
        public Task DeleteOrder(String oid);
    }
}
