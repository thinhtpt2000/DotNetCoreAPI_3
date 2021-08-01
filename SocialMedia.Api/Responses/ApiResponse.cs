using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Api.Responses
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            this.Data = data;
        }
        
        public T Data { get; set; }

        public MetaData meta { get; set; }
    }
}