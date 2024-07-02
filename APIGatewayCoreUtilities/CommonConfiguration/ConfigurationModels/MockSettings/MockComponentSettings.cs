using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayCoreUtilities.CommonConfiguration.ConfigurationModels.MockSettings
{
    public class MockComponentsSettings
    {
        public ContentMetadataServiceMockSettings ContentMetadataServiceMock { get; set; }
        public StreamGatewayMockSettings StreamGatewayMock { get; set; }
    }
}
