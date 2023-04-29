using System.Security;
using Common.Utilities;
using Data.Contracts;
using Entities.User;

namespace Data.Repositories
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        private readonly MyApiContext _context;
        public UserRepository(MyApiContext context):base(context)
        {
            _context = context;
        }


        public async Task AddUser(User user, string password, CancellationToken cancellationToken)
        {
            var hashedPassword = SecurityHelper.HashPasswordSHA256(password);
            user.PasswordHash = hashedPassword;
            await AddAsync(user, cancellationToken);
        }
    }
}
