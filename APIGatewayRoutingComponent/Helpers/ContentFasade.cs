using APIGatewayRouting.Data;
using APIGatewayRouting.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayRouting.Helpers
{
    internal class ContentFasade : IContentFasade
    {
        async Task<bool> IContentFasade.DeleteContentAsync(Guid contentId)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IContentFasade.EditContentAsync(Guid contentId, Content content)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Content>> IContentFasade.GetAllContentsAsync(int limit, int offset)
        {
            throw new NotImplementedException();
        }

        Task<Content> IContentFasade.GetContentByNameAsync(string contentName)
        {
            throw new NotImplementedException();
        }

        async Task<bool> IContentFasade.UploadContentAsync(Content content, ContentLicense license)
        {
            throw new NotImplementedException();
        }
    }
}
