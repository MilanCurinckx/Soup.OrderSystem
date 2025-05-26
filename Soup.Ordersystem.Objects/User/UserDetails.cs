using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects.User
{
    public class UserDetails
    {
        [Key]
        [ForeignKey(nameof(Users))]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PassWordHash {  get; set; }
        public virtual Users Users { get; set; }
    }
}
