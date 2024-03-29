﻿using HotelAPI.Contracts;
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
            string sqlQuery = "INSERT INTO Members (MId, FirstName, LastName, Gender, Birth, Phone, Email, Password, IDNum, Country, City) VALUES (@MId, @FirstName, @LastName, @Gender, @Birth, @Phone, @Email, @Password, @IDNum, @Country, @City)";
            var parameters = new DynamicParameters();
            Guid MId = Guid.NewGuid();
            parameters.Add("MId", MId, DbType.Guid);
            parameters.Add("FirstName", member.FirstName, DbType.String);
            parameters.Add("LastName", member.LastName, DbType.String);
            parameters.Add("Gender", member.Gender, DbType.String);
            parameters.Add("Birth", member.Birth, DbType.Date);
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
                    MId = MId,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    Gender = member.Gender,
                    Birth = member.Birth,
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
            var sqlQuery = "UPDATE Members SET FirstName = @FirstName, LastName = @LastName, Gender = @Gender, Birth = @Birth, Phone = @Phone, Password = @Password, Country = @Country, City = @City WHERE MId = @mid";
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
                    MId = mid,
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
            parameters.Add("mid", mid, DbType.Guid);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }

        /// <summary>
        ///查詢指定Order的Mid 所在Member資料(訂單找會員)
        /// </summary>
        public async Task<Member> GetMemberByOrderMId(String oid)
        {
            // 設定要被呼叫的stored procedure 名稱
            var procedureName = "ShowMemberForProvidedOrderId"; 
            var parameters = new DynamicParameters();
            parameters.Add("Id", oid, DbType.String, ParameterDirection.Input); 
            using (var connection = new SqlConnection(_connectionString)) 
            { 
                var member = await connection.QueryFirstOrDefaultAsync<Member>
                    (procedureName, parameters, commandType: CommandType.StoredProcedure); 
                return member; 
            }
        }

        /// <summary>
        /// 查詢指定Member所屬的所有Order資料
        /// </summary>
        public async Task<Member> GetMemberOrderMultipleResults(Guid mid)
        { 
            var sqlQuery = "SELECT * FROM Members WHERE MId = @Id;" +
                "SELECT * FROM Orders WHERE MId = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", mid, DbType.Guid);
            using (var connection = new SqlConnection(_connectionString)) 
            using (var multi = await connection.QueryMultipleAsync(sqlQuery, parameters)) 
            { 
                var Member = await multi.ReadSingleOrDefaultAsync<Member>(); 
                if (Member != null)
                    Member.Orders = (await multi.ReadAsync<Order>()).ToList(); 
                return Member;
            }
        }

        /// <summary>
        /// 利用身分證後四碼及生日查詢會員資料
        /// </summary>
        public async Task<Member> GetMemberByIDNumAndBirth(String partIDNum, String date)
        {
            string sqlQuery = String.Format("SELECT * FROM Members WHERE IDNum LIKE '______{0}' AND Birth = '{1}'", partIDNum, date);
            // var parameters = new DynamicParameters();
            // parameters.Add("partIDNum", partIDNum, DbType.String);
            // parameters.Add("date", date, DbType.String);
            using (var connection = new SqlConnection(_connectionString))
            {
                var member = await connection.QuerySingleOrDefaultAsync<Member>(sqlQuery);
                return member;
            }
        }

        ///<summary>
        ///利用電子郵件帳號及帳號查詢會員資料(登入)
        ///</summary>
        //public async Task<Member> Login(Member test)
        //{
        //    var sqlQuery = "SELECT * FROM Memebrs WHERE Email = @Email AND Password = @Pwd";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("Email", test.Email, DbType.String);
        //    parameters.Add("Pwd", test.Password, DbType.String);
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        var member = await connection.QueryFirstOrDefaultAsync<Member>(sqlQuery, parameters);
        //        return member;
        //    }
        //}
    }
}


