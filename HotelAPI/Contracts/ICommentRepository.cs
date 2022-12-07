using HotelAPI.Dtos;
using HotelAPI.Models;
using System.Xml.Linq;

namespace HotelAPI.Contracts
{
    public interface ICommentRepository
    {
        /// <summary>
        /// 查詢所有評論資料
        /// </summary>
        public Task<IEnumerable<Comment>> GetMembers();

        /// <summary>
        /// 查詢指定ID的單一評論資料
        /// </summary>
        public Task<Comment> GetMember(Guid id);

        /// <summary>
        /// 修改指定ID的Comment資料
        /// </summary>
        public Task<Comment> UpdateMember(Guid id, CommentForUpdateDto comment);




    }
}
