﻿using System.Net.Http;
using Raven.Client.Documents.Conventions;
using Raven.Client.Http;
using Raven.Client.Json.Converters;
using Sparrow.Json;

namespace Raven.Client.Server.Operations.Configuration
{
    public class GetClientConfigurationOperation : IServerOperation<GetClientConfigurationOperation.Result>
    {
        public RavenCommand<Result> GetCommand(DocumentConventions conventions, JsonOperationContext context)
        {
            return new GetClientConfigurationCommand();
        }

        internal class GetClientConfigurationCommand : RavenCommand<Result>
        {
            public override bool IsReadRequest => false;

            public override HttpRequestMessage CreateRequest(ServerNode node, out string url)
            {
                url = $"{node.Url}/configuration/client";

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get
                };

                return request;
            }

            public override void SetResponse(BlittableJsonReaderObject response, bool fromCache)
            {
                if (response == null)
                    return;

                Result = JsonDeserializationClient.ClientConfigurationResult(response);
            }
        }

        public class Result
        {
            public long RaftCommandIndex { get; set; }

            public ClientConfiguration Configuration { get; set; }
        }
    }
}