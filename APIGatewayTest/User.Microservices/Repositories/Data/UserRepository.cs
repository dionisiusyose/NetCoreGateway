using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Microservices.Context;
using User.Microservices.Models;

namespace User.Microservices.Repositories.Data
{
    public class UserRepository : GeneralRepository<Users, MyContext>
    {
        private readonly MyContext myContext;
        public UserRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        //Login
        public async Task<List<Users>> GetAll()
        {
            //var login = await myContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Email == entity.Email);
            //if (BCrypt.Net.BCrypt.Verify(entity.Password, login.Password))
            //{
            //    return login;
            //}
            return await myContext.Users.ToListAsync();
        }
    }
}
