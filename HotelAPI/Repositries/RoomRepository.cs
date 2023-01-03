using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace HotelAPI.Repositries
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString = DBUtil.ConnectionString();

        /// <summary>
        /// 查詢所有房間資料
        /// </summary>
        public async Task<IEnumerable<Room>> GetRooms()
        {
            string sqlQuery = "SELECT * FROM Rooms";
            using (var connection = new SqlConnection(_connectionString))
            {
                var rooms = await connection.QueryAsync<Room>(sqlQuery);
                return rooms.ToList();
            }
        }

        /// <summary>
        /// 查詢指定ID的單一房間資料
        /// </summary>
        public async Task<Room> GetRoom(String  rid)
        {
            string sqlQuery = "SELECT * FROM Rooms WHERE RId = @rid";
            var parameters = new DynamicParameters();
            parameters.Add("rid", rid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                var room = await connection.QuerySingleOrDefaultAsync<Room>(sqlQuery, parameters);
                return room;
            }
        }

        /// <summary>
        /// 新增Room資料
        /// </summary>
        public async Task<Room> CreateRoom(RoomForCreationDto room)
        {
            string sqlQuery = "INSERT INTO Rooms (RId, RName, RSquare, RBed, RPeople, RFloor, RPrice, RMark) VALUES (@RId, @RName, @RSquare, @RBed, @RPeople, @RFloor, @RPrice, @RMark)";
            var parameters = new DynamicParameters();
            parameters.Add("RId", room.RId, DbType.String);
            parameters.Add("RName",room.RName, DbType.String);
            parameters.Add("RSquare", room.RSquare, DbType.Single);
            parameters.Add("RBed", room.RBed, DbType.Int16);
            parameters.Add("RPeople", room.RPeople, DbType.Int16);
            parameters.Add("RFloor", room.RFloor, DbType.String);
            parameters.Add("RPrice", room.RPrice, DbType.Int16);
            parameters.Add("RMark", room.RMark, DbType.String);
           

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdRoom = new Room
                {
                    RId = room.RId,
                    RName = room.RName,
                    RSquare = room.RSquare,
                    RBed = room.RBed,
                    RPeople = room.RPeople,
                    RFloor = room.RFloor,
                    RPrice = room.RPrice,
                    RMark = room.RMark,
                   
                };
                return createdRoom;
            }

        }

        /// <summary>
        /// 修改指定ID的Room資料
        /// </summary>
        public async Task<Room> UpdateRoom(String rid, RoomForUpdateDto room)
        {
            var sqlQuery = "UPDATE Rooms SET RName = @RName, RSquare = @RSquare, RBed = @RBed, RPeople = @RPeople, RFloor = @RFloor, RPrice = @RPrice, RMark = @RMark WHERE RId = @RId";
            var parameters = new DynamicParameters();
            parameters.Add("RId", rid, DbType.String);
            parameters.Add("RName", room.RName, DbType.String);
            parameters.Add("RSquare", room.RSquare, DbType.Single);
            parameters.Add("RBed", room.RBed, DbType.Int16);
            parameters.Add("RPeople", room.RPeople, DbType.Int16);
            parameters.Add("RFloor", room.RFloor, DbType.String);
            parameters.Add("RPrice", room.RPrice, DbType.Int16);
            parameters.Add("RMark", room.RMark, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var updatedRoom = new Room
                {
                    RId = rid,
                    RName = room.RName,
                    RSquare = room.RSquare,
                    RBed = room.RBed,
                    RPeople = room.RPeople,
                    RFloor = room.RFloor,
                    RPrice = room.RPrice,
                    RMark = room.RMark,
                };
                return updatedRoom;
            }
        }

        /// <summary>
        /// 刪除指定ID的Room資料
        /// </summary>
        public async Task DeleteRoom(String rid)
        {
            var sqlQuery = "DELETE FROM Rooms WHERE RId = @rid";
            var parameters = new DynamicParameters();
            parameters.Add("rid", rid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }

    }
}
