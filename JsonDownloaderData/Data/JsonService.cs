
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JsonDownloaderData.Data;
public class JsonService
{
    private readonly HttpClient httpClient;

    public JsonService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task DownloadAndSaveJson(string apiUrl)
    {
        var json = await httpClient.GetStringAsync(apiUrl);
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var jsonItems = JsonSerializer.Deserialize<List<JsonItem>>(json, options);

        using (var dbContext = new AppDbContext())
        {
            await dbContext.JsonItems.AddRangeAsync(jsonItems);
            await dbContext.SaveChangesAsync();
        }
    }
}
