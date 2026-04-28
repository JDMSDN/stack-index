using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace StackIndex.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentController : ControllerBase
{
    [HttpGet("url")]
    public Results<Ok<string>, InternalServerError> GetPresignedUrlAsync(IAmazonS3 client)
    {
        string url = string.Empty;

        try
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = "stack-index",
                Key = "sample.txt",
                Verb = HttpVerb.PUT,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            url = client.GetPreSignedURL(request);
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return TypedResults.InternalServerError();
        }

        return TypedResults.Ok(url);
    }
}
