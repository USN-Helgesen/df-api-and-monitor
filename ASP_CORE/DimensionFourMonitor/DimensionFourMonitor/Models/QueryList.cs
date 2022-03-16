using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Client.Abstractions;

namespace DimensionFourMonitor.Models
{
    public class QueryList
    {
        private readonly IGraphQLClient _client;
        public QueryList(IGraphQLClient client)
        {
            _client = client;
        }

        public async Task<List<Space>> GetAllSpaces()
        {
            var query = new GraphQLRequest
            {
                Query = @"
                        query LIST_SPACES_WITH_POINTS {
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
            };
            var response = await _client.SendQueryAsync<ResponseSpaceCollectionType>(query);
            return response.Data.Spaces;
        }
    }
}
