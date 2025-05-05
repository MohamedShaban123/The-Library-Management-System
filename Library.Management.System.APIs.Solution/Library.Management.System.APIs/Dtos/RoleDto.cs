using System.ComponentModel.DataAnnotations;

namespace Library.Management.System.APIs.Dtos
{
    public class RoleDto
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string roleName { get; set; }
    }
}
