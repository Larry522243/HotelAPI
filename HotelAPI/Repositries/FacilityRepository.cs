using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace HotelAPI.Repositries
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly string _connectionString = DBUtil.ConnectionString();

        /// <summary>
        /// 查詢所有設施資料
        /// </summary>
        public async Task<IEnumerable<Facility>> GetFacilities()
        {
            string sqlQuery = "SELECT * FROM Facilities";
            using (var connection = new SqlConnection(_connectionString))
            {
                var facilities = await connection.QueryAsync<Facility>(sqlQuery);
                return facilities.ToList();
            }
        }

        /// <summary>
        /// 查詢指定ID的單一設施資料
        /// </summary>
        public async Task<Facility> GetFacility(int fid)
        {
            string sqlQuery = "SELECT * FROM Facilities WHERE FId = @fid";
            var parameters = new DynamicParameters();
            parameters.Add("fid", fid, DbType.Int16);

            using (var connection = new SqlConnection(_connectionString))
            {
                var facility = await connection.QuerySingleOrDefaultAsync<Facility>(sqlQuery, parameters);
                return facility;
            }
        }

        /// <summary>
        /// 新增Facility資料
        /// </summary>
        public async Task<Facility> CreateFacility(FacilityForCreationDto facility)
        {
            string sqlQuery = "INSERT INTO Facilities (FName, FFloor, FTime, FPeople) VALUES (@FName, @FFloor, @FTime, @FPeople)";
            var parameters = new DynamicParameters();
            parameters.Add("FName", facility.FName, DbType.String);
            parameters.Add("FFloor", facility.FFloor, DbType.String);
            parameters.Add("FTime", facility.FTime, DbType.String);
            parameters.Add("FPeople", facility.FPeople, DbType.Int16);


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdFacility = new Facility
                {
                    FName = facility.FName,
                    FFloor = facility.FFloor,
                    FTime = facility.FTime,
                    FPeople = facility.FPeople,

                };
                return createdFacility;
            }

        }

        /// <summary>
        /// 修改指定ID的Facility資料
        /// </summary>
        public async Task<Facility> UpdateFacility(int fid, FacilityForUpdateDto facility)
        {
            var sqlQuery = "UPDATE Facilities SET FName = @FName,FFloor = @FFloor, FTime = @FTime, FPeople = @FPeople WHERE FId = @FId";
            var parameters = new DynamicParameters();
            parameters.Add("FId", fid, DbType.Int16);
            parameters.Add("FName", facility.FName, DbType.String);
            parameters.Add("FFloor", facility.FFloor, DbType.Int16);
            parameters.Add("FTime", facility.FTime, DbType.String);
            parameters.Add("FPeople", facility.FPeople, DbType.Int16);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var updatedFacility = new Facility
                {
                    FId = fid,
                    FTime = facility.FTime,
                    FPeople = facility.FPeople,
                };
                return updatedFacility;
            }
        }

        /// <summary>
        /// 刪除指定ID的Facility資料
        /// </summary>
        public async Task DeleteFacility(int fid)
        {
            var sqlQuery = "DELETE FROM  Facilities WHERE FId = @FId";
            var parameters = new DynamicParameters();
            parameters.Add("FId", fid, DbType.Int16);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
            }
        }
    }
}
