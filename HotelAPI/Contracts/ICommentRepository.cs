using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface ICommentRepository
    {
        //查詢所有評論資料
        public Task<IEnumerable<Comment>> GetComments();

        //查詢指定ID的單一評論資料
        public Task<Comment> GetComment(int Cid);

        // 新增Comment資料
        public Task<Comment> CreateComment(CommentForCreationDto Comment);

        //修改指定ID的Comment資料
        public Task<Comment> UpdateComment(int Cid, CommentForUpdateDto comment);

        //刪除指定ID的Comment資料
        public Task DeleteComment(int Cid);

        //查詢指定Member的Mid 所在Comment資料
        public Task<Comment> GetCommentByMemberMId(Guid Mid);

        // 查詢所有的Comments，以及它底下的所有Members資料
        public Task<List<Comment>> GetCommentsMembersMultipleMapping();
    }
}
