using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;

namespace StackIndex.Components.Pages;

public partial class Upload
{
    [Inject]
    private HttpClient HttpClient { get; set; } = default!;

    /// <summary>
    /// Event handler to upload the file to object storage.
    /// </summary>
    private async Task UploadFile(InputFileChangeEventArgs e)
    {
        var file = e.File;
        if (file is null) return;

        // Fetch the presigned S3 URL.
        var presignedUrl = await HttpClient.GetFromJsonAsync<string>($"Document/url");

        await using var fileStream = file.OpenReadStream();

        // Use the presigned URL to upload the file to S3.
        using (var content = new StreamContent(fileStream))
        {
            content.Headers.ContentLength = fileStream.Length;
            content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            var response = await HttpClient.PutAsync(presignedUrl, content);
        }
    }
}
