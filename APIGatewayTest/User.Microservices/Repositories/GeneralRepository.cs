using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using User.Microservices.Base;
using User.Microservices.Context;
using User.Microservices.Repositories.Interface;

namespace User.Microservices.Repositories
{
    public class GeneralRepository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity
        where TContext : MyContext
    {
        private readonly MyContext myContext;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await myContext.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            myContext.Set<TEntity>().Remove(entity);
            await myContext.SaveChangesAsync();
            return entity;
        }

        //Forgot
        public async Task<TEntity> Patch(TEntity entity)
        {
            Guid id = Guid.NewGuid();
            string guid = id.ToString();
            entity.Password = BCrypt.Net.BCrypt.HashPassword(guid);
            myContext.Entry(entity).State = EntityState.Modified;
            await myContext.SaveChangesAsync();

            string passwordText = "Password Has Been Changed To " + guid;
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("dionisiusyose11@gmail.com", "gmaildion1997");
            MailMessage mm = new MailMessage("donotreply@gmail.com", entity.Email, "Secret!!!!", passwordText);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
            return entity;
        }

        //Login
        public async Task<TEntity> Get(TEntity entity)
        {
            var login = await myContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Email == entity.Email);
            if (BCrypt.Net.BCrypt.Verify(entity.Password, login.Password))
            {
                return login;
            }
            return login;
        }

        //Create account
        public async Task<TEntity> Post(TEntity entity)
        {
            var password = entity.Password;
            entity.Password = BCrypt.Net.BCrypt.HashPassword(password);
            await myContext.Set<TEntity>().AddAsync(entity);
            await myContext.SaveChangesAsync();
            return entity;
        }

        //Change
        public async Task<TEntity> Put(TEntity entity)
        {
            var password = entity.Password;
            entity.Password = BCrypt.Net.BCrypt.HashPassword(password);
            myContext.Entry(entity).State = EntityState.Modified;
            await myContext.SaveChangesAsync();
            return entity;
        }
    }
}
