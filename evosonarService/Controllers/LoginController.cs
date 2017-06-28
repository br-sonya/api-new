using evosonarService.DataObjects;
using evosonarService.Helpers;
using evosonarService.Models;
using Microsoft.Azure.Mobile.Server.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace evosonarService.Controllers
{
    [Route(".auth/login")]
    public class LoginController : ApiController
    {
        private evosonarContext db;
        private string signingKey, audience, issuer;

        public LoginController()
        {
            db = new evosonarContext();
            signingKey = ConfigurationManager.AppSettings["SigningKey"];
            audience = ConfigurationManager.AppSettings["ValidAudience"];
            issuer = ConfigurationManager.AppSettings["ValidIssuer"];
        }

        public IHttpActionResult PostLogin([FromBody] LoginRequest body)
        {
            if (body == null || body.Email == null || body.Password == null ||
                body.Email.Length == 0 || body.Password.Length == 0)
            {
                return BadRequest(); ;
            }

            var user = IsValidUser(body);
            if (user == null)
            {
                return Unauthorized();
            }

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, body.Email)
            };

            JwtSecurityToken token = AppServiceLoginHandler.CreateToken(
                claims, signingKey, audience, issuer, TimeSpan.FromDays(30));
            return Ok(new LoginResult()
            {
                AuthenticationToken = token.RawData,
                User = new LoginResultUser { UserId = user.Id }
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private User IsValidUser(LoginRequest user)
        {
            try
            {
                var password = Sha256Helper.Encode(user.Password);
                return db.Users.Where(u => u.Email.Equals(user.Email) && u.Password.Equals(password)).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class LoginResult
    {
        [JsonProperty(PropertyName = "authenticationToken")]
        public string AuthenticationToken { get; set; }

        [JsonProperty(PropertyName = "user")]
        public LoginResultUser User { get; set; }
    }

    public class LoginResultUser
    {
        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
}