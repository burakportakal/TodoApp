using System;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.SigningCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;
using TodoApp.Core.Security;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TodoApp.CommandHandlers.SigingCommandHandlers
{
    public class UserLoginCommandHandler : CommandHandler<UserLoginCommand, UserLoginResponse>
    {
        private readonly TodoDBContext _context;
        private readonly AppSettings _appSettings;
        public UserLoginCommandHandler(TodoDBContext context, IOptions<AppSettings> appSettings)
        {

            _context = context;
            _appSettings = appSettings.Value;
        }
        protected override Task<UserLoginResponse> ProcessCommand(UserLoginCommand command)
        {
            var response = new UserLoginResponse();
            var user = _context.AcUsers.FirstOrDefault(e => e.UserName == command.Data.UserName);
            if (user == null)
                throw new Exception("Incorrect username or password");

            var security = new SecurityHelper();

            var result = security.ValidateHash(command.Data.Password, user.Salt, user.Password);

            if (!result)
                throw new Exception("Incorrect username or password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.Token = tokenHandler.WriteToken(token);

            return Task.FromResult(response);
        }
    }
}
