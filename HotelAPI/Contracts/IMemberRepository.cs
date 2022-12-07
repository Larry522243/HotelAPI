using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IMemberRepository
    {
        /// <summary>
        /// 查詢所有會員資料
        /// </summary>
        public Task<IEnumerable<Member>> GetMembers();

        /// <summary>
        /// 查詢指定ID的單一會員資料
        /// </summary>
        public Task<Member> GetMember(Guid id);

        /// <summary>
        /// 新增Member資料
        /// </summary>
        public Task<Member> CreateMember(MemberForCreationDto member);

        /// <summary>
        /// 修改指定ID的Member資料
        /// </summary>
        public Task<Member> UpdateMember(Guid id, MemberForUpdateDto member);

        /// <summary>
        /// 刪除指定ID的Member資料
        /// </summary>
        public Task DeleteMember(Guid id);

        




    }
}
