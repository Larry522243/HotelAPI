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
        public async Task<Room> GetRoom(Guid rid)
        {
            string sqlQuery = "SELECT * FROM Rooms WHERE RId = @rid";
            var parameters = new DynamicParameters();
            parameters.Add("rid", rid, DbType.Guid);

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
            Guid RId = Guid.NewGuid();
            parameters.Add("RId", RId, DbType.String);
            parameters.Add("RName",room.RName, DbType.String);
            parameters.Add("RSquare", room.RSquare, DbType.Single);
            parameters.Add("RBed", room.RBed, DbType.UInt64);
            parameters.Add("RPeople", room.RPeople, DbType.UInt64);
            parameters.Add("RFloor", room.RFloor, DbType.String);
            parameters.Add("RPrice", room.RPrice, DbType.UInt64);
            parameters.Add("RMark", room.RMark, DbType.String);
           

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdRoom = new Room
                {
                    RId = RId,
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
        public async Task<Room> UpdateRoom(Guid id, RoomForUpdateDto room)
        {
            var sqlQuery = "UPDATE Rooms SET RId = @RId, RName = @RName, RSquare = @RSquare, RBed = @RBed, RPeople = @RPeople, RFloor = @RFloor, RPrice = @RPrice, RMark = @RMark WHERE RId = @rid";
            var parameters = new DynamicParameters();
            parameters.Add("RId", room.RId, DbType.String);
            parameters.Add("RName", room.RName, DbType.String);
            parameters.Add("RSquare", room.RSquare, DbType.Single);
            parameters.Add("RBed", room.RBed, DbType.DateTime);
            parameters.Add("RPeople", room.RPeople, DbType.UInt64);
            parameters.Add("RFloor", room.RFloor, DbType.String);
            parameters.Add("RPrice", room.RPrice, DbType.UInt64);
            parameters.Add("RMark", room.RMark, DbType.String);
            parameters.Add("RId", id, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var updatedMember = new Member
                {
                    RId = RId,
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
        public async Task DeleteRoom(Guid rid)
        {
            var sqlQuery = "DELETE FROM Rooms WHERE RId = @rid";
            var parameters = new DynamicParameters();
            parameters.Add("rid", rid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }

        /// <summary>
        ///查詢指定OrderDetails的Rid 所在Room資料(訂單明細找房間)
        /// </summary>
        public async Task<Room> GetRoomByOrderDetailsRId(Guid rid)
        {
            // 設定要被呼叫的stored procedure 名稱
            var procedureName = "ShowRoomForProvidedOrderDetailsId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", rid, DbType.Guid, ParameterDirection.Input);
            using (var connection = new SqlConnection(_connectionString))
            {
                var room = await connection.QueryFirstOrDefaultAsync<Room>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return room;
            }
        }

        /// <summary>
        ///查詢指定OrderDetails所屬的所有Room資料
        /// </summary>
        public async Task<Room> GetRoomOrderDetailsMultipleResults(Guid rid)
        {
            var sqlQuery = "SELECT * FROM Rooms WHERE RId = @Id;" +
                "SELECT * FROM OrderDetails WHERE RId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", rid, DbType.Guid);
            using (var connection = new SqlConnection(_connectionString))
            using (var multi = await connection.QueryMultipleAsync(sqlQuery, parameters))
            {
                var Room = await multi.ReadSingleOrDefaultAsync<Room>();
                if (Room != null)
                    Room.OrderDetails = (await multi.ReadAsync<OrderDetails>()).ToList();
                return Room;
            }
        }

    }
}
