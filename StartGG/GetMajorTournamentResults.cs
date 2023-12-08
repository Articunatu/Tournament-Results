using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;


namespace StartGG
{
    public static class GetMajorTournamentResults
    {
        [FunctionName("GetMajorTournamentResults")]

        public static async Task<string> Run(HttpRequestMessage req, ILogger log)
        {
            // Parse the request body for tournament titles
            dynamic data = await req.Content.ReadAsAsync<object>();
            string[] tournamentTitles = data.titles;

            // GraphQL query
            string graphQLQuery = @"
        query TournamentQuery($slug: String, $page: Int!, $perPage: Int!) {
          tournament(slug: $slug) {
            events {
              name
              standings(query: { page: $page, perPage: $perPage }) {
                nodes {
                  standing
                  entrant {
                    name
                  }
                }
              }
            }
          }
        }
    ";

            // Create a GraphQL request payload with the tournament titles
            string requestBody = System.Text.Json.JsonSerializer.Serialize(new
            {
                query = graphQLQuery,
                variables = new
                {
                    slug = "", // Placeholder for tournament slug
                    page = 1,
                    perPage = 3
                }
            });

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.start.gg/graphql"); // Replace with the API endpoint
                client.DefaultRequestHeaders.Add("Authorization", "Bearer YOUR_API_KEY"); // Replace with your API key

                foreach (var title in tournamentTitles)
                {
                    // Update the slug in the request payload for each tournament title
                    var request = new HttpRequestMessage(HttpMethod.Post, "");
                    request.Content = new StringContent(requestBody.Replace("\"slug\": \"\"", $"\"slug\": \"{title}\""), System.Text.Encoding.UTF8, "application/json");

                    // Send the GraphQL request
                    HttpResponseMessage response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        // Process the result (e.g., deserialize JSON and extract necessary data)
                        log.LogInformation(result);
                    }
                    else
                    {
                        log.LogError($"Failed to retrieve data for {title}. Status code: {response.StatusCode}");
                    }
                }
            }

            return "Process completed";
        }

    }
}
