using Microsoft.AspNetCore.Components;
using MudBlazor;
using MUDBLAZOR_DATAGRID.Client.Utils.Features;
using MUDBLAZOR_DATAGRID.Client.Utils.HttpRepository;
using MUDBLAZOR_DATAGRID.Shared.Models;
using MUDBLAZOR_DATAGRID.Shared.RequestFeatures;

namespace MUDBLAZOR_DATAGRID.Client.Pages
{
	public partial class FetchData
	{
		private MudTable<Product> table = new MudTable<Product>();
		private ProductParameters productParameters = new ProductParameters();
		private readonly int[] pageSizeOption = { 4, 6, 10 };

		[Inject]
		public IHttpClientRepository Repository { get; set; } = null!;

		private async Task<TableData<Product>> GetServerData(TableState state)
		{
			productParameters.PageSize = state.PageSize;
			productParameters.PageNumber = state.Page + 1;

			productParameters.OrderBy = state.SortDirection == SortDirection.Descending 
				? state.SortLabel + " desc" : state.SortLabel;
			
			PagingResponse<Product> response = await Repository.GetProducts(productParameters);

			return new TableData<Product>
			{
				Items = response.Items,
				TotalItems = response.MetaData.TotalCount
			};
		}

		private void OnSearch(string searchTerm)
		{
			productParameters.SearchTerm = searchTerm;
			table.ReloadServerData();
		}
	}
}
