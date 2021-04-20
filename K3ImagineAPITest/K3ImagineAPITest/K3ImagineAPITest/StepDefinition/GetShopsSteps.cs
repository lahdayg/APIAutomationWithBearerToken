using System;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using NUnit.Framework;

namespace K3ImagineAPITest.StepDefinition
{
    [Binding]
    public class GetShopsSteps

    {
         RestClient client;
         RestRequest request;
         IRestResponse response;
         static dynamic token;
      


       [Given(@"I navigate to the website and I send a POST request to get the bearer token")]
        public void GivenINavigateToTheWebsiteAndISendAPOSTRequestToGetTheBearerToken()
        {
            var client = new RestClient("https://portal.k3imagine.com/gw-portal/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=password&username=akin&password=PasswordSecure44@", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string content = Regex.Unescape(response.Content);
            content = content.Substring(1, content.Length - 2);
            token = JObject.Parse(content);
        }

        [When(@"I send a GET request to receive the list of shops")]
        public void WhenISendAGETRequestToReceiveTheListOfShops()
        {
            client = new RestClient("https://portal.k3imagine.com/gw-rbo/api/v1.0/shop?includeInactive=true&includePrimaryGroupName=true");
            request = new RestRequest(Method.GET);
            request.AddHeader("authorization", "Bearer " + token.access_token);
            request.AddHeader("cache-control", "no-cache");
            response = client.Execute(request);

        }

        [Then(@"I should receive a response that shop Koge exist")]
        public void ThenIShouldReceiveAResponseThatShopKogeExist()
        {
            var dataArray = JArray.Parse(response.Content);
            //bool findKoge = false;
            foreach (var jToken in dataArray)
            {
                var item = (JObject)jToken;
                if (item.GetValue("description").ToString() == "Koge")
                    Assert.That(item.GetValue("description").ToString().Contains("Koge"), Is.True); 
            }
        }
    }
}
