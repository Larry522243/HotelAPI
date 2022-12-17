using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface ICommentRepository
    {
        //查詢所有評論資料
        public Task<IEnumerable<Comment>> GetMembers();

        //查詢指定ID的單一評論資料
        public Task<Comment> GetMember(Guid Cid);

        // 新增Comment資料
        public Task<Comment> CreateComment(CommentForCreationDto Comment);

        //修改指定ID的Comment資料
        public Task<Comment> UpdateMember(Guid Cid, CommentForUpdateDto comment);


        //查詢指定Member的Mid 所在Comment資料
        public Task<Comment> GetCommentByMemberMId(Guid Mid);

        // 查詢所有的Comment，以及它底下的所有Member資料
        public Task<List<Comment>> GetCommentsMembersMultipleMapping();
    }
}
