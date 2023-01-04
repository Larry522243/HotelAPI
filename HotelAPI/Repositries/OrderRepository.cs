using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System;

namespace HotelAPI.Repositries
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString = DBUtil.ConnectionString();

        /// <summary>
        /// 查詢所有訂單資料
        /// </summary>

        public async Task<IEnumerable<Order>> GetOrders()
        {
            string sqlQuery = "SELECT * FROM Orders";
            using (var connection = new SqlConnection(_connectionString))
            {
                var orders = await connection.QueryAsync<Order>(sqlQuery);
                return orders.ToList();
            }
        }

        /// <summary>
        /// 查詢指定id的訂單資料
        /// </summary>

        public async Task<Order> GetOrder(String oid)
        {
            string sqlQuery = "SELECT * FROM Orders WHERE OId = @oid";
            var parameters = new DynamicParameters();
            parameters.Add("oid", oid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                var order = await connection.QuerySingleOrDefaultAsync<Order>(sqlQuery, parameters);
                return order;
            }

        }

        /// <summary>
        /// 新增Order資料
        /// </summary>

        public async Task<Order> CreateOrder(OrderForCreationDto order)
        {
            string sqlQuery = "INSERT INTO Orders (OId, MId, CreateDate, CheckIn, CheckOut, ORoomCnt, OPeopleCnt) VALUES(@OId, @MId, @CreateDate, @CheckIn, @CheckOut, @ORoomCnt, @OPeopleCnt)";
            var parameters = new DynamicParameters();
            string OId = DateTime.Now.ToString("yyMMddHHmmssff");
            parameters.Add("OId", OId, DbType.String);
            parameters.Add("MId", order.MId, DbType.Guid);
            parameters.Add("CreateDate", order.CreateDate, DbType.DateTime);
            parameters.Add("CheckIn", order.CheckIn, DbType.DateTime);
            parameters.Add("CheckOut", order.CheckOut, DbType.DateTime);
            parameters.Add("ORoomCnt", order.ORoomCnt, DbType.Int16);
            parameters.Add("OPeopleCnt", order.OPeopleCnt, DbType.Int16);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdOrder = new Order
                {
                    OId = OId,
                    MId = order.MId,
                    CreateDate = order.CreateDate,
                    CheckIn = order.CheckIn,
                    CheckOut = order.CheckOut,
                    ORoomCnt = order.ORoomCnt,
                    OPeopleCnt = order.OPeopleCnt,
                   
                };
                return createdOrder;
            }


        }

        /// <summary>
        /// 修改指定id的訂單資料
        /// </summary>

        public async Task<Order> UpdateOrder(String oid, OrderForUpdateDto order)
        {
            var sqlQuery = "UPDATE Orders SET OId = @OId, CheckIn = @CheckIn, CheckOut = @CheckOut, ORoomCnt = @ORoomCnt, OPeopleCnt = @OPeopleCnt WHERE OId = @oid";
            var parameters = new DynamicParameters();
            string OId = DateTime.Now.ToString("yyMMddHHmmssff");
            parameters.Add("OId", OId, DbType.String);
            parameters.Add("CheckIn", order.CheckIn, DbType.DateTime);
            parameters.Add("CheckOut", order.CheckOut, DbType.DateTime);
            parameters.Add("ORoomCnt", order.ORoomCnt, DbType.Int16);
            parameters.Add("OPeopleCnt", order.OPeopleCnt, DbType.Int16);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdOrder = new Order
                {
                    OId = OId,
                    CheckIn = order.CheckIn,
                    CheckOut = order.CheckOut,
                    ORoomCnt = order.ORoomCnt,
                    OPeopleCnt = order.OPeopleCnt,

                };
                return createdOrder;
            }
        }

        /// <summary>
        /// 刪除指定ID的訂單資料
        /// </summary>

        public async Task DeleteOrder(String oid)
        {
            var sqlQuery = "DELETE FROM Orders WHERE OId = @oid";
            var parameters = new DynamicParameters();
            parameters.Add("oid", oid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
    }
}
