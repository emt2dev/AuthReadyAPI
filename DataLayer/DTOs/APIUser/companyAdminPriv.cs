using System.ComponentModel.DataAnnotations;

namespace AuthReadyAPI.DataLayer.DTOs.APIUser
{
    public class companyAdminPriv
    {
        public string userEmail { get;set; }
        public int companyId { get;set; }
        public int replaceAdminOneOrTwo  { get;set; }
    }
}