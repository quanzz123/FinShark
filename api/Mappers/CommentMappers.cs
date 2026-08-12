using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto toCommentDto(this Comment model)
        {
            return new CommentDto
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                CreateOn = model.CreateOn,
                createdBy = model.AppUser.UserName,
                StockID = model.StockID
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommnentDto dto, int stockId)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content,
                StockID = stockId
            };
        }
        public static Comment ToCommentFromUpdateDto(this UpdateCommentDto dto)
        {
            return new Comment
            {
                Title = dto.Title,
                Content = dto.Content
            };
        }
    
    }
}