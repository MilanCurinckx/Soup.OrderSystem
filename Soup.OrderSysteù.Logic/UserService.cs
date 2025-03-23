using Soup.OrderSystem.Data;
using Soup.OrderSystem.Objects.User;
namespace Soup.OrderSystem.Logic
{
    public class UserService
    {
        //don't need to make a separate repository, I will be using EF as one
        private readonly OrderContext _context;
        public UserService(OrderContext orderContext)
        {
            this._context = orderContext;
        }


    }
}
