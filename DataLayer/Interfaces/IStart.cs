using AuthReadyAPI.DataLayer.DTOs.APIUser;
using AuthReadyAPI.DataLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthReadyAPI.DataLayer.Interfaces
{
    public interface IStart
    {
        Task<string> INIT__START(Full__APIUser initObj);
    }
}
