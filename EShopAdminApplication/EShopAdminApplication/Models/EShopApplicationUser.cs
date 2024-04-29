using System.Net.Sockets;

namespace EShopAdminApplication.Models
{
    public class EShopApplicationUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
    }
}
