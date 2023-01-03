using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IRoomRepository
    {
        //查詢所有房間資料
        public Task<IEnumerable<Room>> GetRooms();

        //查詢指定ID的單一房間資料
        public Task<Room> GetRoom(String rid);

        //新增Room資料
        public Task<Room> CreateRoom(RoomForCreationDto room);

        //修改指定ID的Room資料
        public Task<Room> UpdateRoom(String rid, RoomForUpdateDto room);

        //刪除指定ID的Room資料
        public Task DeleteRoom(String ridd);
    }
}
