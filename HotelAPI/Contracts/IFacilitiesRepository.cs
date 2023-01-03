using HotelAPI.Dtos;
using HotelAPI.Models;

namespace HotelAPI.Contracts
{
    public interface IFacilitiesRepository
    {
        //查詢所有設施資料
        public Task<IEnumerable<Facility>> GetFacilities();

        //查詢指定ID的單一設施資料
        public Task<Facility> GetFacility(Guid Fid);

        //新增Facility資料
        public Task<Facility> CreateFacility(FacilitiesForCreationDto Facility);

        //修改指定ID的Facility資料
        public Task<Facility> UpdateFacility(Guid Fid, FacilitiesForUpdateDto Facility);
    }
}
