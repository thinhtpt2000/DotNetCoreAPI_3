﻿using System;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Infrastructure.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filters, string actionUrl);
    }
}