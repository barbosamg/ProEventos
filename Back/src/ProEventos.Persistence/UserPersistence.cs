using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Persistence
{
    public class UserPersistence : GeralPersistence, IUserPersistence
    {
        private readonly ProEventosContext _eventoContext;
        public UserPersistence(ProEventosContext eventoContext) : base(eventoContext)
        {
            _eventoContext = eventoContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _eventoContext.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _eventoContext.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _eventoContext.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

    }
}
