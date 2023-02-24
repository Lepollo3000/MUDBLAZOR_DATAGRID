using MUDBLAZOR_DATAGRID.Client.Utils.Features;
using MUDBLAZOR_DATAGRID.Shared.Models;
using MUDBLAZOR_DATAGRID.Shared.RequestFeatures;

namespace MUDBLAZOR_DATAGRID.Client.Utils.HttpRepository
{
    public interface IHttpClientRepository
    {
        Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters);
    }
}
