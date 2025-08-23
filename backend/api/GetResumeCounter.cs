using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;

namespace Company.Function
{
    public static class GetResumeCounter
    {
       
        [Function("GetResumeCounter")]
        public static HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDBInput(
            databaseName: "AzureResume",
            containerName: "Counter",
            ConnectionStringSetting = "AzureResumeConnectionString",
            Id = "1",
            PartitionKey = "1")] Counter counter,
            [CosmosDBOutput(
            databaseName: "AzureResume",
            containerName: "Counter",
            Connection  = "AzureResumeConnectionString",
            Id = "1",
            PartitionKey = "1")] out Counter updatedCounter)
        
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            updatedCounter = counter;
            updatedCounter.Count +1

            var jsonToReturn = JsonConvert.SerializeObject(counter);

            return new HttpResponseData(System.Net.HttpStatuscode.OK)
            {
                Content = new StringContent(jsonToReturn, Encoding.UTF8, "application/json")
            };
        }
    }
}
