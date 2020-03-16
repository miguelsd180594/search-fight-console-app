using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.IO;
using System.Net;

namespace SearchFightConsoleApp.Services.ApiClients.Serialization
{
    class NewtonsoftJsonSerializer : IDeserializer
    {
        private JsonSerializer _serializer;
        public NewtonsoftJsonSerializer(JsonSerializer serializer)
        {
            _serializer = serializer;
        }
        public T Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (var stringReader = new StringReader(content))
                {
                    using (var jsonTextReader = new JsonTextReader(stringReader))
                    {
                        return _serializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }
            return (T)Activator.CreateInstance(typeof(T), true);
        }

        public static NewtonsoftJsonSerializer Default
        {
            get
            {
                return new NewtonsoftJsonSerializer(new JsonSerializer
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
        }
    }
}
