using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Options;
using SocialMedia.Core.QueryFilters;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            this._unitOfWork = unitOfWork;
            this._paginationOptions = options.Value;
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            var posts = _unitOfWork.PostRepository.GetAll();
            // Do filters
            var userId = filters.UserId;
            if (userId != null)
            {
                posts = posts.Where(x => x.UserId == userId);
            }
            var date = filters.Date;
            if (date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == date?.ToShortDateString());
            }
            var description = filters.Description;
            if (description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(description.ToLower()));
            }
            // Do paging
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;
            var pagedPost = PagedList<Post>.Create(posts, filters.PageNumber, filters.PageSize);
            return pagedPost;
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User doesn't exist");
            }

            var enumUserPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            var userPost = enumUserPost.ToList();
            if (userPost.Count() < 10)
            {
                var lastPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();
                if (lastPost != null && (DateTime.Now - lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }
            if (post.Description.Contains("Sex"))
            {
                throw new BusinessException("Content not allowed");
            }
            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var existingPost = await _unitOfWork.PostRepository.GetById(post.Id);
            existingPost.Image = post.Image;
            existingPost.Description = post.Description;
            _unitOfWork.PostRepository.Update(existingPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}