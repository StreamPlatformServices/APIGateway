namespace APIGatewayRouting.Helpers.Interfaces
{
    internal interface ISessionTokenCacher
    {
        public bool CheckToken(string userName, string token);
        public void CacheToken(string userName, string token);
    }
}

