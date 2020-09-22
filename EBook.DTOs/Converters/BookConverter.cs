using EBook.Core.Entites;
using EBook.DTOs.Books;
using System;

namespace EBook.DTOs.Converters
{
    public static class BookConverter
    {
        public static BookDto ToDto(this Book entity)
        {
            if (entity == null)
            {
                //抛出一个异常
                throw new ArgumentNullException(nameof(entity));
            }

            var resultDto = new BookDto
            {
                ID = entity.ID,
                Name = entity.Name,
                Price = entity.Price,
                Author = entity.Author
            };


            return resultDto;
        }
    }
}
