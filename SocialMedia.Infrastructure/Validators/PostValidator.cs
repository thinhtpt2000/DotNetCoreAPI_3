using System;
using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .WithMessage("Please input value for description");
            
            RuleFor(post => post.Description)
                .Length(10, 2000)
                .WithMessage("Description must be from 10 - 2000 chars");

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}