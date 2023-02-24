using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using MUDBLAZOR_DATAGRID.Server.Data;
using MUDBLAZOR_DATAGRID.Server.Utils.Paging;
using MUDBLAZOR_DATAGRID.Server.Utils.Repository;
using MUDBLAZOR_DATAGRID.Shared.Models;
using MUDBLAZOR_DATAGRID.Shared.RequestFeatures;

namespace MUDBLAZOR_DATAGRID.Server.Controllers
{
	[ApiController]
	[Route("api/products")]
	public class ProductsController : ControllerBase
	{
		private readonly ApplicationDbContext _dbcontext;

		public ProductsController(ApplicationDbContext context)
		{
			_dbcontext = context;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] ProductParameters productParameters)
		{
			IEnumerable<Product> products = await _dbcontext.Products
				.Search(productParameters.SearchTerm!)
				.Sort(productParameters.OrderBy!)
				.ToListAsync();

			var response = PagedList<Product>.ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);

			Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(response.MetaData));

			return Ok(response);
		}
	}
}