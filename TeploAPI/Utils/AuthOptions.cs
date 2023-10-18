using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TeploAPI.Utils
{
    public class AuthOptions
    {
        // TODO: Remove comments and change key,
        // replace ISSUER and AUDIENCE
        public const string ISSUER = "TeploAuthServer";

        public const string AUDIENCE = "TeploAuthClient";

        const string KEY = "mSR@qTe_mvbgRwyBqgz+3_pY5GxK2AhsZYz?92YfP=8BwUjHL?@3?wM5-yexU8tH";

        public const int LIFETIMEDAYS = 1;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
