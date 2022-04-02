using DarGates.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.Interfaces
{
    public interface IGuestInterface
    {
        Task<ApiResponse<int>> AddOwner(OwnerDTO ownerDTO);
        Task<ApiResponse<bool>> UpdateOwner(OwnerDTO ownerDTO);
        Task<ApiResponse<bool>> DeleteOwner(int ID, string UserID);
        ApiResponse<List<OwnerDTO>> GetOwners();
    }
}
