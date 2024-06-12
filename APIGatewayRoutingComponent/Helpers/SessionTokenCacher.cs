using APIGatewayRouting.Data;
using System.Collections.Generic;

namespace APIGatewayRouting.Helpers.Interfaces
{
    internal class SessionTokenCacher : ISessionTokenCacher
    {
        private readonly ITimeWrapper _timeWrapper;

        //TODO: Is it ok? with this _
        private IDictionary<string, SessionToken> _sessionTokenCache = new Dictionary<string, SessionToken>();

        private const int TOKEN_DURATION_IN_MINUTES = 60;
        
        void ISessionTokenCacher.CacheToken(string userName, string token)
        {
            var sessionToken = new SessionToken
            {
                Token = token,
                ExpirationTime = _timeWrapper.GetCurrentTimeUtc() + TimeSpan.FromMinutes(TOKEN_DURATION_IN_MINUTES)
            };

            _sessionTokenCache.Add(userName, sessionToken);
        }

        bool ISessionTokenCacher.CheckToken(string userName, string token) //TODO: change to login
        {
            ClearExpiredTokens();

            if (_sessionTokenCache.ContainsKey(userName))
            {
                return _sessionTokenCache[userName].Equals(token);
            }

            return false;
        }

        private void ClearExpiredTokens()
        {
            _sessionTokenCache
                .Where(x => x.Value.ExpirationTime < _timeWrapper.GetCurrentTimeUtc())
                .Select(x => x.Key)
                .ToList()
                .ForEach(key => _sessionTokenCache.Remove(key));
        }
    }
}

