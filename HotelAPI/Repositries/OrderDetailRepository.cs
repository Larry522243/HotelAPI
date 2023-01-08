using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace HotelAPI.Repositries
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly string _connectionString = DBUtil.ConnectionString();

        ///<summary>
        ///查詢所有OrderDetail資料
        ///</summary>
        public async Task<IEnumerable<OrderDetail>> GetOrderDetails()
        {
            string sqlQuery = "SELECT * FROM OrderDetails";
            using (var connection = new SqlConnection(_connectionString))
            {
                var orderDetails = await connection.QueryAsync<OrderDetail>(sqlQuery);
                return orderDetails.ToList();
            }
        }

        /// <summary>
        /// 查詢指定OrderID的OrderDetail資料
        /// </summary>
        public async Task<OrderDetail> GetOrderDetail(String oid)
        {
            string sqlQuery = "SELECT * FROM OrderDetails WHERE OId = @OId";
            var parameters = new DynamicParameters();
            parameters.Add("OId", oid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                var orderDetail = await connection.QuerySingleOrDefaultAsync<OrderDetail>(sqlQuery, parameters);
                return orderDetail;
            }
        }

        /// <summary>
        /// 批次新增OrderDetail資料
        /// </summary>
        public async Task CreateMultipleOrderDetails(List<OrderDetailForCreationDto> orderDetails)
        {
            var query = "INSERT INTO OrderDetails (OId, RId) VALUES (@OId, @RId)";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var orderDetail in orderDetails)
                    {
                        var parameters = new DynamicParameters();
                        parameters.Add("OId", orderDetail.OId, DbType.String);
                        parameters.Add("RId", orderDetail.RId, DbType.Guid);
                        await connection.ExecuteAsync(query, parameters, transaction);
                    }
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// 批次修改指定ID的OrderDetail資料
        /// </summary>
        //public async Task UpdateMultipleOrderDetails(String oid, List<OrderDetailForUpdateDto> orderDetails)
        //{
        //    var query = "UPDATE OrderDetails SET OId = @OId, RID = @RId";

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();

        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            foreach (var orderDetail in orderDetails) {
        //                var parameters = new DynamicParameters();
        //                parameters.Add("OId", orderDetail.OId, DbType.String);
        //                parameters.Add("RId", orderDetail.RId, DbType.String);
        //                await connection.ExecuteAsync(query, parameters, transaction);
        //            }
        //            transaction.Commit();
        //        }
        //    }
        //}

        /// <summary>
        /// 刪除指定ID的OrderDetail資料
        /// </summary>
        public async Task DeletOrderDetail(String oid)
        {
            var sqlQuery = "DELETE FROM OrderDetails WHERE OId = @OId";
            var parameters = new DynamicParameters();
            parameters.Add("OId", oid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
    }
}
