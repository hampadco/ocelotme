

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtTokenHandler
{

    public const string KeySecret = "Xyzhtjkdfghjadhsfgjka";

    public const int ExpireTime = 30;

    public List<UserAccount> UserAccounts ;
    



    public JwtTokenHandler()
    {
        UserAccounts = new List<UserAccount>();
        UserAccounts.Add(new UserAccount { UserName = "admin", Password = "admin" , Role = "Admin"});
        UserAccounts.Add(new UserAccount { UserName = "user", Password = "user" , Role = "User"});

    }

    //generate token
    public AutanticationResponse GenerateToken(AutanticationRequest request)
    {
        var user = UserAccounts.FirstOrDefault(x => x.UserName == request.UserName && x.Password == request.Password);
        
        if (user == null)
        {
            return null;
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(KeySecret);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(ExpireTime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AutanticationResponse { 
            Token = tokenHandler.WriteToken(token),
            ExpireTime = ExpireTime,
            UserName = user.UserName,
            Role = user.Role

            
             };




    }




    
}