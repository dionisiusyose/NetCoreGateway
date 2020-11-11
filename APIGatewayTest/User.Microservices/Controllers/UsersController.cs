using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using User.Microservices.Base;
using User.Microservices.Models;
using User.Microservices.Repositories.Data;

namespace User.Microservices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController<Users, UserRepository>
    {
        public UserRepository repository;
        public UsersController(UserRepository repository, IConfiguration configuration) : base(repository, configuration)
        {
            this.repository = repository;
        }

        [HttpGet(nameof(GetAll))]
        public async Task<ActionResult<Users>> GetAll()
        {
            var result = await repository.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(new { data = result });
        }
    }
}
