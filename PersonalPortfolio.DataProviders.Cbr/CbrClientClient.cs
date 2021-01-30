using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Xml;

namespace PersonalPortfolio.DataProviders.Cbr
{
    /// <inheritdoc cref="ICbrClient" />
    internal class CbrClientClient : ClientBase<ICbrClient>, ICbrClient
    {
        public CbrClientClient(Binding binding, string endpoint) :
            base(binding, new EndpointAddress(endpoint))
        {
        }

        public Task<XmlNode> GetCursOnDateXMLAsync(DateTime onDate) =>
            Channel.GetCursOnDateXMLAsync(onDate);

        public static Binding DefaultBindingForEndpoint()
        {
            var result = new CustomBinding();
            var textBindingElement =
                new TextMessageEncodingBindingElement
                {
                    MessageVersion = MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None)
                };
            result.Elements.Add(textBindingElement);
            var httpBindingElement =
                new HttpTransportBindingElement
                {
                    AllowCookies = true,
                    MaxBufferSize = int.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue
                };
            result.Elements.Add(httpBindingElement);
            return result;
        }
    }
}