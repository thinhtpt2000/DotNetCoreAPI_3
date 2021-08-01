using System;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infrastructure.Interfaces;

namespace SocialMedia.Infrastructure.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            this._baseUri = baseUri;
        }

        public Uri GetPostPaginationUri(PostQueryFilter filters, string actionUrl)
        {
            var baseUrl = $"{_baseUri}{actionUrl}";
            return new Uri(baseUrl);
        }
    }
}