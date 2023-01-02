using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IRoomReposiroty
    {
        //查詢所有房間資料
        public Task<IEnumerable<Room>> GetRooms();

        //查詢指定ID的單一房間資料
        public Task<Room> GetRoom(Guid rid);

        //新增Room資料
        public Task<Room> CreateRoom(RoomForCreationDto room);

        //修改指定ID的Room資料
        public Task<Room> UpdateRoom(Guid rid, RoomForUpdateDto room);

        //刪除指定ID的Room資料
        public Task DeleteRoom(Guid rid);

        //查詢指定OrderDetails的Rid 所在Room資料(訂單明細找房間)
        public Task<Room> GetRoomByOrderDetailsRId(Guid rid);

        // 查詢指定OrderDetails所屬的所有Room資料
        public Task<Room> GetRoomOrderDetailsMultipleResults(Guid rid);
    }
}
