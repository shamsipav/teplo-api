﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeploAPI.Utils;

namespace TeploAPI.Utils
{
    public static class Validate
    {
        internal static string ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidAudience = AuthOptions.AUDIENCE
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                string accountEmail = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;

                if (accountEmail != null)
                    return accountEmail;

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
