using P2.MVC.AuthModels;
using P2.MVC.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using NLog;

namespace P2.MVC.Filters
{
    public class GetAccountDetailsFilter : IAsyncActionFilter
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        //private readonly Logger logger;

        public GetAccountDetailsFilter(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            //logger = LogManager.GetLogger("allfile");
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // do something before the action executes
            // if the controller is an aservicecontroller, then
            // fetch the details, otherwise, do nothing.
            //logger.Info("Running Get Account Filter");
            if (context.Controller is AServiceController controller)
            {
                HttpRequestMessage request = controller.CreateRequestToService(
                HttpMethod.Get, "api/Users/Details");

                HttpResponseMessage response = await controller.HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    // setting "Result" in a filter short-circuits the rest
                    // of the MVC pipeline
                    // but i won't do that, i should just log it.
                }
                else
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    AuthAccountDetails details = JsonConvert.DeserializeObject<AuthAccountDetails>(jsonString);
                    controller.ViewData["accountDetails"] = details;
                    controller.Account = details;
                }
            }

            await next();
        }
    }
}
