using System.Web.Http;
using System.Web.Http.Tracing;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Config;
using System.Web.Http.Controllers;
using evosonarService.Models;
using evosonarService.DataObjects;
using System.Threading.Tasks;
using evosonarService.Helpers;
using System.Linq;
using System;

namespace evosonarService.Controllers
{
    // Use the MobileAppController attribute for each ApiController you want to use  
    // from your mobile clients 
    [MobileAppController]
    public class UserController : TableController<User>
    {
        private evosonarContext _context;
        // GET api/values
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            _context = new evosonarContext();
            DomainManager = new EntityDomainManager<User>(_context, Request);
        }

        // GET tables/User/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [Authorize]
        public SingleResult<User> GetUser(string id)
        {
            return Lookup(id);
        }

        // POST tables/User
        public async Task<IHttpActionResult> PostUser([FromBody]User user)
        {
            var u = Query().Where(s=> s.Email.Equals(user.Email)).FirstOrDefault();
            if (u != null)
                throw new Exception("O usuário já existe");
            user.Password = Sha256Helper.Encode(user.Password);
            User current = await InsertAsync(user);
            return Ok();
        }
    }
}
