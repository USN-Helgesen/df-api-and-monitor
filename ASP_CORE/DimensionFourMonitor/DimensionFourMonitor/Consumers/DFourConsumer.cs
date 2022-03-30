using System;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DimensionFourMonitor.ResponseTypes;
using DimensionFourMonitor.Models;

namespace DimensionFourMonitor.Consumers
{
    public class DFourConsumer
    {
        public readonly HttpClient _httpClient;
        
        public DFourConsumer(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<Space>> GetAllSpaces()
        {
            var queryObject = new
            {
                query = @"query LIST_SPACES_WITH_POINTS {
                    spacesConnection {
                        edges {
                        node {
                            id
                            name
                            points {
                            id
                            name
                            }
                        }
                    }
                    }
                }",
                variables = new { }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(queryObject), Encoding.UTF8, "application/json")
            };

            dynamic responseObj;

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                responseObj = JsonConvert.DeserializeObject<dynamic>(responseString);
            }
            List<Space> spaceList = new List<Space>();
            foreach (JObject element in responseObj.data.spacesConnection.edges)
            {
                if (element != null)
                {
                    var firstNode = element.First;
                    if (firstNode != null)
                    {
                        var secondNode = firstNode.First;
                        if (secondNode != null)
                        {
                            Space space = new Space(secondNode);
                            spaceList.Add(space);
                        }
                    }
                }
            }
            return spaceList;
        }
    }
}