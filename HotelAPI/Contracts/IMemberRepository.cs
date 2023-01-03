using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IMemberRepository
    {
        //查詢所有會員資料
        public Task<IEnumerable<Member>> GetMembers();

        //查詢指定ID的單一會員資料
        public Task<Member> GetMember(Guid mid);

        //新增Member資料
        public Task<Member> CreateMember(MemberForCreationDto member);

        //修改指定ID的Member資料
        public Task<Member> UpdateMember(Guid mid, MemberForUpdateDto member);

        //刪除指定ID的Member資料
        public Task DeleteMember(Guid mid);

        //查詢指定Order的Mid 所在Member資料(訂單找會員)
        public Task<Member> GetMemberByOrderMId(String oid);

        // 查詢指定Member所屬的所有Order資料
        public Task<Member> GetMemberOrderMultipleResults(Guid mid);

        //利用身分證後四碼及生日查詢會員資料
        public Task<Member> GetMemberByIDNumAndBirth(String partIDNum, String date);
    }
}
