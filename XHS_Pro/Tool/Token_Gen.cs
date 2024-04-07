using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace XHS_Pro.Tool
{
    public class Token_Gen:Token_Fe
    {
        private readonly TokenOption_Format token;
        public Token_Gen(TokenOption_Format token)
        {
            this.token = token;
        }

        public string CreateToken(string user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, user));
            DateTime expires = DateTime.Now.AddMinutes(token.AccessTokenExpiresMinutes);
            var Rtoken = new JwtSecurityToken(
           issuer: token.Issuer,
           audience: token.Audience,
           claims: claims,           //携带的荷载
           notBefore: DateTime.Now,  //token生成时间
           expires: expires,         //token过期时间
           signingCredentials: new SigningCredentials(
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token.IssuerSigningKey)), SecurityAlgorithms.HmacSha256
               )
           );
            return new JwtSecurityTokenHandler().WriteToken(Rtoken);
        }
    }
}
