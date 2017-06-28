using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using evosonarService.DataObjects;
using evosonarService.Models;
using System.Collections.Generic;

namespace evosonarService.Controllers
{
    [Authorize]
    public class AddressController : TableController<Address>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            evosonarContext context = new evosonarContext();
            DomainManager = new EntityDomainManager<Address>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<Address> GetAllAddress()
        {

            return Query();
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public List<Address> GetAddress(string userId)
        {
            return Query().Where(a=> a.UserId.Equals(userId)).ToList();
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Address> PatchAddress(string id, Delta<Address> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostAddress(Address item)
        {
            Address current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteAddress(string id)
        {
            return DeleteAsync(id);
        }
    }
}