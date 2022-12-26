using BestUnitPrice.Application.Common.Models;

namespace BestUnitPriceApp.Common.Extensions
{
    internal static class HttpResponseMessageExtensions
    {
        public static Result ToApplicationResult(this HttpResponseMessage result)
        {
            return result.IsSuccessStatusCode
                ? Result.Success()
                : Result.Failure(new[] { result.StatusCode.ToString(), result.ReasonPhrase });
        }


    }
}