using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.OrderSystem.Objects.User
{
    public class UserDetails
    {
        [Key]
        [ForeignKey(nameof(Users))]
        [PersonalData]
        public int UserId { get; set; }
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        [PersonalData]
        public string? PassWordHash {  get; set; }
        public virtual Users Users { get; set; }
    }
}
