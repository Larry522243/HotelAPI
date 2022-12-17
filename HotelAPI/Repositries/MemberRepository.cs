using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace HotelAPI.Repositries
{
    public class MemberRepository : IMemberRepository
    {
        private readonly string _connectionString = DBUtil.ConnectionString();

        /// <summary>
        /// 查詢所有會員資料
        /// </summary>
        public async Task<IEnumerable<Member>> GetMembers()
        {
            string sqlQuery = "SELECT * FROM Members";
            using (var connection = new SqlConnection(_connectionString))
            {
                var members = await connection.QueryAsync<Member>(sqlQuery);
                return members.ToList();
            }
        }

        /// <summary>
        /// 查詢指定ID的單一會員資料
        /// </summary>
        public async Task<Member> GetMember(Guid mid)
        {
            string sqlQuery = "SELECT * FROM Members WHERE MId = @mid";
            var parameters = new DynamicParameters();
            parameters.Add("mid", mid, DbType.Guid);

            using (var connection = new SqlConnection(_connectionString))
            {
                var member = await connection.QuerySingleOrDefaultAsync<Member>(sqlQuery, parameters);
                return member;
            }
        }

        /// <summary>
        /// 新增Member資料
        /// </summary>
        public async Task<Member> CreateMember(MemberForCreationDto member)
        {
            string sqlQuery = "INSERT INTO Members (FirstName, LastName, Gender, Birth, Phone, Email, Password, IDNum, Country, City) VALUES (@FirstName, @LastName, @Gender, @Birth, @Phone, @Email, @Password, @IDNum, @Country, @City)";
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", member.FirstName, DbType.String);
            parameters.Add("LastName", member.LastName, DbType.String);
            parameters.Add("Gender", member.Gender, DbType.String);
            parameters.Add("Birth", member.Birth, DbType.DateTime);
            parameters.Add("Phone", member.Phone, DbType.String);
            parameters.Add("Email", member.Email, DbType.String);
            parameters.Add("Password", member.Password, DbType.String);
            parameters.Add("IDNum", member.IDNum, DbType.String);
            parameters.Add("Country", member.Country, DbType.String);
            parameters.Add("City", member.City, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdMember = new Member
                {
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Gender = member.Gender,
                    Phone = member.Phone,
                    Email = member.Email,
                    Password = member.Password,
                    IDNum = member.IDNum,
                    Country = member.Country,
                    City = member.City
                };
                return createdMember;
            }

        }

        /// <summary>
        /// 修改指定ID的Member資料
        /// </summary>
        public async Task<Member> UpdateMember(Guid mid, MemberForUpdateDto member)
        {
            var sqlQuery = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender, Birth = @Birth, Phone = @Phone, Email = @Email, Password = @Password, Country = @Country, City = @City WHERE MId = @mid";
            var parameters = new DynamicParameters();
            parameters.Add("FirstName", member.FirstName, DbType.String);
            parameters.Add("LastName", member.LastName, DbType.String);
            parameters.Add("Gender", member.Gender, DbType.String);
            parameters.Add("Birth", member.Birth, DbType.DateTime);
            parameters.Add("Phone", member.Phone, DbType.String);
            parameters.Add("Password", member.Password, DbType.String);
            parameters.Add("Country", member.Country, DbType.String);
            parameters.Add("City", member.City, DbType.String);
            parameters.Add("MId", mid, DbType.Guid);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var updatedMember = new Member
                {
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Gender = member.Gender,
                    Birth = member.Birth,
                    Phone = member.Phone,
                    Password = member.Password,
                    Country = member.Country,
                    City = member.City,
                };
                return updatedMember;
            }
        }

        /// <summary>
        /// 刪除指定ID的Member資料
        /// </summary>
        public async Task DeleteMember(Guid mid)
        {
            var sqlQuery = "DELETE FROM Members WHERE MId = @mid";
            var parameters = new DynamicParameters();
            parameters.Add("mid", mid, DbType.String);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }

        /// <summary>
        ///查詢指定Order的Mid 所在Member資料(訂單找會員)
        /// </summary>
        public async Task<Member> GetMemberByOrderMId(Guid mid)
        {
            // 設定要被呼叫的stored procedure 名稱
            var procedureName = "ShowMemberForProvidedOrderId"; 
            var parameters = new DynamicParameters();
            parameters.Add("Id", mid, DbType.Guid, ParameterDirection.Input); 
            using (var connection = new SqlConnection(_connectString)) 
            { 
                var company = await connection.QueryFirstOrDefaultAsync<Member>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure); 
                return company; 
            }
        }

        /// <summary>
        /// 查詢指定Member所屬的所有Order資料
        /// </summary>
        public async Task<Member> GetMemberOrderMultipleResults(Guid mid)
        { 
            var query = "SELECT * FROM Member WHERE MId = @Id;" +
                "SELECT * FROM Order WHERE MemberId = @Id"; 
            using (var connection = new SqlConnection(_connectString))
            using (var multi = await connection.QueryMultipleAsync(query, new { mid })) 
            { 
                var Member = await multi.ReadSingleOrDefaultAsync<Member>(); 
                if (Member != null)
                    Member.Orders = (await multi.ReadAsync<Order>()).ToList(); 
                return Member;
            }
        }
        //






    }



}


