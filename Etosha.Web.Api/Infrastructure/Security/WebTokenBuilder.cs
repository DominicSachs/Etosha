using Etosha.Server.Common.Extensions;
using Etosha.Server.Common.Models;
using Etosha.Server.Common.Validation;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Etosha.Web.Api.Infrastructure.Security
{
  internal class WebTokenBuilder : IWebTokenBuilder
  {
    private readonly JwtIssuerOptions _jwtOptions;

    public WebTokenBuilder(IOptions<JwtIssuerOptions> jwtOptions)
    {
      _jwtOptions = jwtOptions.Value;
      ValidateJwtOptions(_jwtOptions);
    }

    public string GenerateToken(User user)
    {
      var claims = new[]
      {
          new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
          new Claim(ClaimTypes.Name, user.UserName),
          new Claim(JwtRegisteredClaimNames.Email, user.Email),
          new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.Jti.ToString()),
          new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64),
      };

      var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static void ValidateJwtOptions(JwtIssuerOptions jwtOptions)
    {
      Require.ThatNotNull(jwtOptions, nameof(jwtOptions));
      Require.ThatNotNull(jwtOptions.SigningCredentials, nameof(JwtIssuerOptions.SigningCredentials));
      Require.ThatNotNull(jwtOptions.Jti, nameof(JwtIssuerOptions.Jti));

      if (jwtOptions.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
      }
    }
  }
}
