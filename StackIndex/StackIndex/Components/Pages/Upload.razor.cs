using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace StackIndex.Components.Pages;

public partial class Upload
{
    [Inject]
    private HttpClient HttpClient { get; set; } = default!;

    private async Task UploadFile(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file is null) return;

        var presignedUrl = await HttpClient.GetFromJsonAsync<string>($"Document/url");

        await using var fileStream = file.OpenReadStream();

        using var content = new StreamContent(fileStream);

        content.Headers.ContentLength = fileStream.Length;

        content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        var response = await HttpClient.PutAsync(presignedUrl, content);
    }
}
