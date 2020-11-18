using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pokedex.Model;
using Refit;

namespace Pokedex.Services
{
    public class RestClientAPI : IRestClientAPI
    {
        private readonly IRestClientAPI _restClient;

        public RestClientAPI()
        {
            _restClient = RestService.For<IRestClientAPI>(RestEndPoints.BaseUrl);
        }

        async Task<List<Root>> IRestClientAPI.GetAll()
        {
            return await _restClient.GetAll();
        }
    }
}
