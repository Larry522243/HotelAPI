using HotelAPI.Contracts;
using HotelAPI.Dtos;
using HotelAPI.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace HotelAPI.Repositries
{
    public class FacilitiesRepository : IFacilitiesRepository
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
        /// 查詢指定ID的單一設施資料(注意Int16 fid)
        /// </summary>
        public async Task<Facility> GetFacility(Int16 fid)
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
        public async Task<Facility> CreateFacility(FacilitiesForCreationDto facility)
        {
            string sqlQuery = "INSERT INTO Facilities (FId, FName, FFloor, FTime, FPeople) VALUES (@FId, @FName, @FFloor, @FTime, @FPeople)";
            var parameters = new DynamicParameters();
            parameters.Add("FId", facility.FId, DbType.Int16);
            parameters.Add("FName", facility.FName, DbType.String);
            parameters.Add("FFloor", facility.FFloor, DbType.String);
            parameters.Add("FTime", facility.FTime, DbType.String);
            parameters.Add("FPeople", facility.FPeople, DbType.Int16);


            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var createdFacility = new Facility
                {
                    FId = facility.FId,
                    FName = facility.FName,
                    FFloor = facility.FFloor,
                    FTime = facility.FTime,
                    FPeople = facility.FPeople,

                };
                return createdFacility;
            }

        }

        /// <summary>
        /// 修改指定ID的Facility資料(注意Dto修改的地方&注意Int16 fid)
        /// </summary>
        public async Task<Facility> UpdateFacility(Int16 fid, FacilitiesForUpdateDto facility)
        {
            var sqlQuery = "UPDATE Facilities SET  FTime = @FTime, FPeople = @FPeople WHERE FId = @FId";
            var parameters = new DynamicParameters();
            //parameters.Add("FId", fid, DbType.Int16);
            parameters.Add("FTime", facility.FTime, DbType.String);
            parameters.Add("FPeople", facility.FPeople, DbType.Int16);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sqlQuery, parameters);
                var updatedFacility = new Facility
                {
                    //FId = fid,
                    FTime = facility.FTime,
                    FPeople = facility.FPeople,
                };
                return updatedFacility;
            }
        }

    }
}
