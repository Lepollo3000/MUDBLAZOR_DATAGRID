using Microsoft.AspNetCore.WebUtilities;
using MUDBLAZOR_DATAGRID.Client.Utils.Features;
using MUDBLAZOR_DATAGRID.Shared.Models;
using MUDBLAZOR_DATAGRID.Shared.RequestFeatures;
using System.Text.Json;

namespace MUDBLAZOR_DATAGRID.Client.Utils.HttpRepository
{
    public class HttpClientRepository : IHttpClientRepository
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public HttpClientRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["pageSize"] = productParameters.PageSize.ToString(),
                ["searchTerm"] = productParameters.SearchTerm ?? "",
                ["orderBy"] = productParameters.OrderBy ?? ""
            };

            using (var response = await _client.GetAsync(QueryHelpers.AddQueryString("products", queryStringParam)))
            {
                if (response.IsSuccessStatusCode)
                {
                    MetaData? metaData = JsonSerializer
                        .Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options);

                    Stream stream = await response.Content.ReadAsStreamAsync();

                    var pagingResponse = new PagingResponse<Product>()
                    {
                        Items = await JsonSerializer.DeserializeAsync<List<Product>>(stream, _options) ?? null!,
                        MetaData = metaData!
                    };

                    return pagingResponse;
                }

                var nullPagingResponse = new PagingResponse<Product>
                {
                    Items = null!,
                    MetaData = null!
                };

                return nullPagingResponse;
            }
        }
    }
}
