using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using System.Xml.Linq;

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
        public async Task<Comment> GetComment(Int16 cid)
        {
            string sqlQuery = "SELECT * FROM Comments WHERE CId = @cid";
            var parameters = new DynamicParameters();
            parameters.Add("cid", cid, DbType.Int16);

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
            string sqlQuery = "INSERT INTO Comments (CId, MId, CreateDate, Star, Content) VALUES (@CId, @MId, @CreateDate, @Star, @Content)";
            var parameters = new DynamicParameters();
            parameters.Add("CId", comment.CId, DbType.Int16);
            parameters.Add("MId", comment.MId, DbType.String);
            parameters.Add("CreateDate", comment.CreateDate, DbType.DateTime);
            parameters.Add("Star", comment.Star, DbType.Int16);
            parameters.Add("Content", comment.Content, DbType.String);


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdComment = new Comment
                {
                    CId = comment.CId,
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
        public async Task<Comment> UpdateComment(Int16 cid, CommentForUpdateDto comment)
        {
            var sqlQuery = "UPDATE Comments SET CreateDate = @CreateDate, Star = @Star, Content = @Content WHERE CId = @CId";
            var parameters = new DynamicParameters();
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
        ///查詢指定Member的Mid 所在Comment資料
        /// </summary>
        public async Task<Comment> GetCommentByMemberMId(String mid)
        {
            // 設定要被呼叫的stored procedure 名稱
            var procedureName = "ShowCommentForProvidedMemberId";
            var parameters = new DynamicParameters();
            parameters.Add("Id", mid, DbType.Guid, ParameterDirection.Input);
            using (var connection = new SqlConnection(_connectionString))
            {
                var comment = await connection.QueryFirstOrDefaultAsync<Comment>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure);
                return comment;
            }
        }

        /// <summary>
        ///查詢所有的Comments，以及它底下的所有Members資料
        /// </summary>
        public async Task<List<Comment>> GetCommentsMembersMultipleMapping()
        {
            var query = "SELECT * FROM Comments c JOIN Member m ON c.Id = m.CommentsId";
            using (var connection = new SqlConnection(_connectionString))
            {
                var commentDict = new Dictionary<Int16, Comment>();
                var comments = await connection.QueryAsync<Comment, Member, Comment>(
                query, (comment, member) =>
                {
                    if (!commentDict.TryGetValue(comment.Id, out var currentComment))
                    {
                        currentComment = comment;
                        commentDict.Add(currentComment.Id, currentComment);
                    }
                    currentComment.Employees.Add(member);
                    return currentComment;
                }
                );
                return comments.Distinct().ToList();
            }
        }


    }
}
