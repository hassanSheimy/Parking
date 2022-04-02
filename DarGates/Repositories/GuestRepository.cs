using DarGates.DB;
using DarGates.Interfaces;
using DarGates.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace DarGates.Repositories
{
    public class GuestRepository : IGuestInterface
    {
        private readonly DBContext _context;
        private readonly IBasicServices _services;
        private readonly IMapper _mapper;
        public GuestRepository(DBContext context, IBasicServices services, IMapper mapper)
        {
            _context = context;
            _services = services;
            _mapper = mapper;
        }
        #region Owners / Visitors
        public async Task<ApiResponse<int>> AddOwner(OwnerDTO ownerDTO)
        {
            var response = new ApiResponse<int>();
            try
            {
                var owner = new Owner
                {
                    Type = ownerDTO.Type,
                    Price = ownerDTO.Price,
                    PriceInHoliday = ownerDTO.PriceInHoliday
                };

                await _context.Owner.AddAsync(owner);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Success";
                response.Response = owner.Id;
                return response;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "AddOwner");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> UpdateOwner(OwnerDTO ownerDTO)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var owner = await _context.Owner.FindAsync(ownerDTO.Id);
                if (owner == null)
                {
                    response.Status = false;
                    response.Message = "Invalid ID";
                    return response;
                }
                owner.Price = ownerDTO.Price;
                owner.PriceInHoliday = ownerDTO.PriceInHoliday;
                owner.Type = ownerDTO.Type ?? owner.Type;
                _context.Owner.Update(owner);
                await _context.SaveChangesAsync();

                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "UpdateOwner");
            }
            return response;
        }
        public async Task<ApiResponse<bool>> DeleteOwner(int ID, string UserID)
        {
            var response = new ApiResponse<bool>();
            try
            {
                var owner = await _context.Owner.FindAsync(ID);
                if (owner == null)
                {
                    response.Status = false;
                    response.Message = "Invalid ID";
                    return response;
                }
                owner.IsDeleted = true;
                owner.DeletedByUserId = UserID;
                //_context.Remove(owner);
                _context.Update(owner);
                await _context.SaveChangesAsync();
                response.Status = true;
                response.Message = "Success";
                response.Response = true;
            }
            catch (Exception ex)
            {
                await _services.SaveErrorLog(ex.InnerException.ToString(), "DeleteOwner");
            }
            return response;
        }
        public ApiResponse<List<OwnerDTO>> GetOwners()
        {
            var response = new ApiResponse<List<OwnerDTO>>();
            var owners = _context.Owner.Skip(2).Where(o => !o.IsDeleted).Select(_mapper.Map<OwnerDTO>).ToList();
            if (owners.Count == 0)
            {
                response.Status = true;
                response.Message = "null";
                return response;
            }
            response.Status = true;
            response.Message = "Success";
            response.Response = owners;
            return response;
        }
        #endregion
    }
}
