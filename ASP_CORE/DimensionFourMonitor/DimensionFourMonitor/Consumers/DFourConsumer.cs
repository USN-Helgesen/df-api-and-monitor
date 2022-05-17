using System;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using DimensionFourMonitor.Models;

namespace DimensionFourMonitor.Consumers
{
    public class DFourConsumer
    {
        private HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;

        public DFourConsumer(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;

        }
        public void EstablishCredentials()
        {
            if(_session.GetString("Tenant Id") != null || _session.GetString("Tenant Key") != null)
            {
                string tenantId = _session.GetString("Tenant Id");
                string tenantKey = _session.GetString("Tenant Key");
                _httpClient.DefaultRequestHeaders.Add("x-tenant-id", tenantId);
                _httpClient.DefaultRequestHeaders.Add("x-tenant-key", tenantKey);
            }
        }
        public async Task<List<Space>> GetAllSpaces()
        {
            var queryObject = new
            {
                query = @"query LIST_SPACES_WITH_POINTS {
                    spaces {
                        edges {
                            node {
                                id
                                name
                                points {
                                    edges {
                                        node {
                                            id
                                            name
                                        }
                                    }
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
                Console.WriteLine(responseObj);
            }
            List<Space> spaceList = new List<Space>();
            foreach (JObject element in responseObj.data.spaces.edges)
            {
                if (element != null)
                {
                    var firstNode = element.First;
                    if (firstNode != null)
                    {
                        var secondNode = firstNode.First;
                        if (secondNode != null)
                        {
                            Space space = new Space(secondNode, this);
                            spaceList.Add(space);
                        }
                    }
                }
            }
            return spaceList;
        }
        public async Task<List<Space>> GetTopLevelSpaces()
        {
            var queryObject = new
            {
                query = @"query LIST_SPACES_WITH_POINTS {
                    spaces {
                        edges {
                            node {
                                id
                                name
                                parent {
                                    id
                                }
                                children {
                                    edges {
                                        node {
                                            id
                                            name
                                        }
                                    }
                                }
                                points {
                                    edges {
                                        node {
                                            id
                                            name
                                        }
                                    }
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
            foreach (JObject element in responseObj.data.spaces.edges)
            {
                if (element != null)
                {
                    var firstNode = element.First;
                    if (firstNode != null)
                    {
                        var secondNode = firstNode.First;
                        if (secondNode != null && secondNode["parent"].HasValues == false)
                        {
                            Space space = new Space(secondNode, this);
                            spaceList.Add(space);
                        }
                    }
                }
            }
            return spaceList;
        }
        public async Task<Space> GetSpace(string spaceId)
        {
            var queryObject = new
            {
                query = @"query LIST_SPACES_WITH_POINTS(
                            $spaceId: String!
                        ){
                              spaces(where: { id: { _EQ: $spaceId}}){
                                edges {
                                  node {
                                    id
                                    name
                                    children {
                                        edges {
                                            node {
                                                id
                                            }
                                        }
                                    }
                                    points {
                                      edges {
                                        node {
                                          id
                                          name
                                        }
                                      }
                                    }
                                  }
                                }
                              }
                            }",
                variables = new
                {
                    spaceId = spaceId
                }
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
            Space space = null;
            foreach (JObject element in responseObj.data.spaces.edges)
            {
                if (element != null)
                {
                    var firstNode = element.First;
                    if (firstNode != null)
                    {
                        var secondNode = firstNode.First;
                        if (secondNode != null)
                        {
                            space = new Space(secondNode, this);
                        }
                    }
                }
            }
            return space;
        }
        public async Task<Point> GetPoint(string pointId)
        {
            var queryObject = new
            {
                query = @"query GET_MY_POINT(
                                $pointId: String!
                            ){
                              points(where: {id:{_EQ: $pointId}}) {
                                edges {
                                    node {
                                        id
                                        name
                                        metadata
                                        space {
                                            id
                                            name
                                        }
                                    }
                                }
                            }
                        }",
                variables = new
                {
                    pointId = pointId
                }
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
            Point point = null;
            foreach (JObject element in responseObj.data.points.edges)
            {
                if (element != null)
                {
                    var firstNode = element.First;
                    if (firstNode != null)
                    {
                        var secondNode = firstNode.First;
                        if (secondNode != null)
                        {
                            point = new Point(secondNode);
                        }
                    }
                }
            }
            return point;
        }
        public async Task<List<Signal>> GetSignals(string pointId, string type, int paginate)
        {
            string id = pointId;
            string signalType = type;
            int signalPaginate = paginate;
            var queryObject = new
            {
                query = @"query LATEST_SIGNAL_FROM_MY_DEVICE (
                            $deviceId: String!
                            $type: String!
                            $paginate: Int!
                          ){
                            points(where: { id: { _EQ: $deviceId } }) {
                                edges {
                                    node {
                                    temperature: signals(
                                    where: { type: { _EQ: $type } }
                                    paginate: { last: $paginate }
                                    ){
                                    edges {
                                        node {
                                        id
                                        timestamp
                                        createdAt
                                        type
                                        unit
                                        pointId
                                        data {
                                                numericValue
                                                rawValue
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }",
                variables = new
                {
                    deviceId = id,
                    type = signalType,
                    paginate = signalPaginate
                }   
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

            List<Signal> signalList = new List<Signal>();
            if (responseObj.data.points.edges != null)
            {
                var firstNode = responseObj.data.points.edges.First;
                if (firstNode != null)
                {
                    foreach (JObject element in firstNode.node.temperature.edges)
                    {
                        var secondNode = element.First;
                        if (secondNode != null)
                        {
                            var thirdNode = secondNode.First;
                            if (thirdNode != null)
                            {
                                Signal signal = new Signal(thirdNode);
                                signalList.Add(signal);
                            }
                        }
                    }
                }
            }
            return signalList;
        }
        public async Task<bool> CreateSpace(string spaceName, string parentSpaceId)
        {
            var queryObject = new
            {
                query = @"mutation CREATE_SPACE (
                            $parentId: ID!
                            $spaceName: String!
                          ){
                            space {
                              create(
                                input: {
                                  parentId: $parentId
                                  name: $spaceName
                                }){
                                  id
                                  name
                                }
                              }
                            }",
                variables = new
                {
                    parentId = parentSpaceId,
                    spaceName = spaceName
                }
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
            bool responseSuccessBool;
            if(responseObj["errors"] != null)
            {
                responseSuccessBool = false;
            }
            else
            {
                responseSuccessBool = true;
            }
            return responseSuccessBool;
        }
        public async Task<bool> CreatePoint(string pointName, string spaceId)
        {
            var queryObject = new
            {
                query = @"mutation CREATE_POINT (
                            $spaceId: ID!
                            $pointName: String!
                          ){
                            point {
                              create(
                                input: {
                                  spaceId: $spaceId
                                  name: $pointName
                                }
                              ) {
                                id
                                name
                              }
                            }
                          }",
                variables = new
                {
                    pointName = pointName,
                    spaceId = spaceId
                }
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
            bool responseSuccessBool;
            if (responseObj["errors"] != null)
            {
                responseSuccessBool = false;
            }
            else
            {
                responseSuccessBool = true;
            }
            return responseSuccessBool;
        }
        public async Task<bool> UpdatePointTypes(string pointId, List<string> typeList)
        {
            var typeDict = new SortedDictionary<string, List<string>>
            {
                {"types", typeList }
            };
            var queryObject = new
            {
                query = @"mutation UPDATE_METADATA (
                            $pointId: ID!
                            $typeList: JSONObject!
                          ){
                            point {
                              update(
                                input: {
                                  id: $pointId
                                  point: { metadata: $typeList }
                                }
                              ) {
                                id
                                metadata
                              }
                            }
                          }",
                variables = new
                {
                    pointId = pointId,
                    typeList = typeDict
                }
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

            bool responseSuccessBool;
            if (responseObj["errors"] != null)
            {
                responseSuccessBool = false;
            }
            else
            {
                responseSuccessBool = true;
            }
            return responseSuccessBool;
        }
        public async Task<bool> DeleteSpace(string spaceId)
        {
            var queryObject = new
            {
                query = @"mutation DELETE_SPACE (
                            $spaceId: ID!
                          ){
                            space {
                              delete(input: {
                                id: $spaceId
                              }) {
                                id
                              }
                            }
                          }",
                variables = new
                {
                    spaceId = spaceId
                }
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

            bool responseSuccessBool;
            if (responseObj["errors"] != null)
            {
                Console.WriteLine("failed");
                Console.WriteLine(responseObj);
                responseSuccessBool = false;
            }
            else
            {
                Console.WriteLine("Success");
                Console.WriteLine(responseObj);
                responseSuccessBool = true;
            }
            return responseSuccessBool;
        }
        public async Task<bool> DeletePoint(string pointId)
        {
            var queryObject = new
            {
                query = @"mutation DELETE_POINT (
                            $pointId: ID!
                          ){
                            point {
                              delete(input: { 
                                id: $pointId
                              }) {
                                id
                              }
                            }
                          }",
                variables = new
                {
                    pointId = pointId
                }
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

            bool responseSuccessBool;
            if (responseObj["errors"] != null)
            {
                Console.WriteLine("failed");
                Console.WriteLine(responseObj);
                responseSuccessBool = false;
            }
            else
            {
                Console.WriteLine("Success");
                Console.WriteLine(responseObj);
                responseSuccessBool = true;
            }
            return responseSuccessBool;
        }
    }
}