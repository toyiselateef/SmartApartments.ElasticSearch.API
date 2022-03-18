using Application.Interface.Services;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartApartment.API.Helper;
using SmartApartment.API.Models;
using SmartApartment.Application.Interface.Services;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SmartApartment.API.Controllers
{

    [Route("apartments")]
    public class ApartmentSearchController : BaseController
    {
        private readonly ILogger<ApartmentSearchController> logger;
        private readonly ISearchService searchService;
        private readonly IUploadServices uploadService;

        public ApartmentSearchController(ILogger<ApartmentSearchController> logger, ISearchService searchService, IUploadServices uploadService)
        {
            this.logger = logger;
            this.searchService = searchService;
            this.uploadService = uploadService;
        }


        /// <summary>
        /// The list of all available properties' markets
        /// </summary>
        /// <returns>List of all markets that's present in the index.</returns>
        [HttpGet]
        [Route("marketlist")]
        [ProducesResponseType(statusCode: 200)]
        public async Task<ActionResult<APIResponse<List<string>>>> GetAllMarkets()
        {
           
            List<string> result = await searchService.GetMarketsAsync();

            Response<List<string>> APISuccessResponse = new Response<List<string>>(ResponseTypes.success, result);
            logger.LogInformation("search with success");
            return Ok(APISuccessResponse);
        }

        /// <summary>
        /// Searches an apartment data in an index.
        /// </summary>
        /// <param name="query">the term used for search</param>
        /// <returns>List of documents that matches to search query, of type management or property</returns>
        [HttpPost]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(404)]
        [Route("simplesearch")]
        public async Task<ActionResult<APIResponse<List<SearchResult>>>> SearchApartment([FromBody] SearchInputs query)
        {

           
            List<SearchResult> searchResponse = await searchService.SearchAsync(query);

            Response<List<SearchResult>> APISuccessResponse = new Response<List<SearchResult>>(ResponseTypes.success, searchResponse);
            logger.LogInformation("search with success. endpoint: simplesearch");
            return Ok(APISuccessResponse);

        }


        /// <summary>
        /// This fetches an apartment data in an index.
        /// </summary>
        /// <param name="query">the term used for search</param>
        /// <returns>List of documents that matches to search query, of type management or property</returns>
        [HttpPost]
        [Route("indexDocuments")]
        [ProducesResponseType(statusCode: 200)]
        public async Task<ActionResult<APIResponse<IList<string>>>> UploadAndIndexDocuments()
        {

            bool uploadAndIndexResult = await uploadService.IndexDocumentAsync();

            Response<bool> APISuccessResponse = new Response<bool>(ResponseTypes.success, uploadAndIndexResult);
            logger.LogInformation("upload and index was success");
            return Ok(APISuccessResponse);


        }

    }

}
