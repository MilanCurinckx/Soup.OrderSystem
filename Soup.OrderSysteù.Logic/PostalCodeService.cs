using Soup.OrderSystem.Data;
using Soup.OrderSystem.Objects.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soup.OrderSystem.Logic
{
    public class PostalCodeService
    {
        private readonly OrderContext _orderContext;
        public PostalCodeService(OrderContext context)
        {
            this._orderContext = context;
        }
        /// <summary>
        /// Creates a new postal code which can be used in the creation of a new address
        /// </summary>
        /// <param name="postalcode"></param>
        /// <param name="municipality"></param>
        public void CreatePostalCode(int postalcode, string municipality)
        {
            PostalCode postalCode = new PostalCode();
            postalCode.Postal_Code = postalcode;
            postalCode.Municipality = municipality;
            _orderContext.Add(postalCode);
            _orderContext.SaveChanges();
        }

        /// <summary>
        /// returns a list of all the PostalCodes 
        /// </summary>
        /// <returns></returns>
        public List<PostalCode> GetPostalCodeList()
        {
            var list = _orderContext.PostalCodes.ToList();
            return list;
        }
        /// <summary>
        /// Returns a postal code object, postalcode as int is required for parameter
        /// </summary>
        /// <param name="postalcode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        
        // reminder: find only looks for properties with a primary key, postalcode is the primary key for this class
        public PostalCode GetByCode(int postalcode)
        {
            var FoundPostalCode = _orderContext.PostalCodes.Find(postalcode);
            if (FoundPostalCode == null)
            {
                throw new Exception("Could not find postal code");
            }
            else
            {
                return FoundPostalCode;
            }
        }
        /// <summary>
        /// Returns a postal code object, municipality as string is required for parameter
        /// </summary>
        /// <param name="municipality"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public PostalCode GetByMunicipality(string municipality)
        {
            var FoundPostalCode = _orderContext.PostalCodes.FirstOrDefault(c => c.Municipality == municipality);
            if (FoundPostalCode == null)
            {
                throw new Exception("Could not find postal code");
            }
            else
            {
                return FoundPostalCode;
            }
        }
        /// <summary>
        /// Updates the postal code that has been given along
        /// </summary>
        /// <param name="Updated"></param>
        public void Update(PostalCode Updated)
        {
            var toUpdate = _orderContext.PostalCodes.Find(Updated.Postal_Code);
            if (toUpdate == null)
            {
                throw new Exception("Could not find postal code");
            }
            else
            {
                toUpdate.Postal_Code = Updated.Postal_Code;
                toUpdate.Municipality = Updated.Municipality;
                _orderContext.Update(toUpdate);
                _orderContext.SaveChanges();
            }
        }
        /// <summary>
        /// Deletes a postalcode & it's municipality from the database
        /// </summary>
        /// <param name="ToDelete"></param>
        /// <exception cref="Exception"></exception>
        public void Remove(PostalCode postalCode)
        {
            var toDelete = _orderContext.PostalCodes.Find(postalCode.Postal_Code);
            if(toDelete == null)
            {
                throw new Exception("Could not find postal code");
            }
            else 
            {
                _orderContext.Remove(toDelete);
            }
        }
    }
    
}
