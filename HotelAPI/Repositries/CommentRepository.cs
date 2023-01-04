using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace HotelAPI.Repositries
{
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString = DBUtil.ConnectionString();

        /// <summary>
        /// 查詢所有評論資料
        /// </summary>
        public async Task<IEnumerable<Comment>> GetComments()
        {
            string sqlQuery = "SELECT * FROM Comments";
            using (var connection = new SqlConnection(_connectionString))
            {
                var comments = await connection.QueryAsync<Comment>(sqlQuery);
                return comments.ToList();
            }
        }

        /// <summary>
        /// 查詢指定ID的單一評論資料
        /// </summary>
        public async Task<Comment> GetComment(int cid)
        {
            string sqlQuery = "SELECT * FROM Comments WHERE CId = @cid";
            var parameters = new DynamicParameters();
            parameters.Add("cid", cid, DbType.Int64);

            using (var connection = new SqlConnection(_connectionString))
            {
                var comment = await connection.QuerySingleOrDefaultAsync<Comment>(sqlQuery, parameters);
                return comment;
            }
        }

        /// <summary>
        /// 新增Comment資料
        /// </summary>
        public async Task<Comment> CreateComment(CommentForCreationDto comment)
        {
            string sqlQuery = "INSERT INTO Comments (MId, CreateDate, Star, Content) VALUES (@MId, @CreateDate, @Star, @Content)";
            var parameters = new DynamicParameters();
            parameters.Add("MId", comment.MId, DbType.Guid);
            parameters.Add("CreateDate", comment.CreateDate, DbType.DateTime);
            parameters.Add("Star", comment.Star, DbType.Int16);
            parameters.Add("Content", comment.Content, DbType.String);


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdComment = new Comment
                {
                    MId = comment.MId,
                    CreateDate = comment.CreateDate,
                    Star = comment.Star,
                    Content = comment.Content,

                };
                return createdComment;
            }

        }

        /// <summary>
        /// 修改指定ID的Comment資料
        /// </summary>
        public async Task<Comment> UpdateComment(int cid, CommentForUpdateDto comment)
        {
            var sqlQuery = "UPDATE Comments SET CreateDate = @CreateDate, Star = @Star, Content = @Content WHERE CId = @CId";
            var parameters = new DynamicParameters();
            parameters.Add("CId", cid, DbType.Int32);
            parameters.Add("CreateDate", comment.CreateDate, DbType.DateTime);
            parameters.Add("Star", comment.Star, DbType.Int16);
            parameters.Add("Content", comment.Content, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var updatedComment = new Comment
                {
                    CreateDate = comment.CreateDate,
                    Star = comment.Star,
                    Content = comment.Content,
                };
                return updatedComment;
            }
        }

        /// <summary>
        /// 刪除指定ID的Comment資料
        /// </summary>
        public async Task DeleteComment(int cid)
        {
            var sqlQuery = "DELETE FROM Comments WHERE CId = @CId";
            var parameters = new DynamicParameters();
            parameters.Add("CId", cid, DbType.Int32);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.QueryAsync(sqlQuery, parameters);
            }
        }
        /// <summary>
        ///查詢指定Member的Mid 所在Comment資料
        /// </summary>
        public async Task<Comment> GetCommentByMemberMId(Guid mid)
        {
            var sqlQuery = "SELECT c.CId, c.CreateDate, c.Star, c.Content FROM Comments c, Members m WHERE m.MId = @MId AND m.MId = c.MId";
            var parameters = new DynamicParameters();
            parameters.Add("MId", mid, DbType.Guid);

            using(var connection = new SqlConnection(_connectionString))
            {
                var comment = await connection.QuerySingleOrDefaultAsync<Comment>(sqlQuery, parameters);
                return comment; 
            }
        }

        /// <summary>
        ///查詢所有的Comments，以及它底下的所有Members資料
        /// </summary>
        //public async Task<List<Comment>> GetCommentsMembersMultipleMapping()
        //{
        //    var query = "SELECT * FROM Comments c JOIN Members m ON c.MId = m.MId";
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var commentDict = new Dictionary<int, Comment>();
        //        var comments = await connection.QueryAsync<Comment, Member, Comment>(
        //        query, (comment, member) =>
        //        {  
        //            if (!commentDict.TryGetValue(comment.CId, out var currentComment))
        //            {
        //                currentComment = comment;
        //                commentDict.Add(currentComment.CId, currentComment);
        //            }
        //            currentComment.Members.Add(member);
        //            return currentComment;
        //        }
        //        );
        //        return comments.Distinct().ToList();
        //    }
        //}
    }
}
