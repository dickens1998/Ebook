using EBook.DTOs.Books;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EBook.Services.Books
{
    public interface IBookService
    {

        Task<List<BookDto>> GetAllAsync();
        Task<List<BookDto>> GetAllAsync(int userId);
        Task<BookDto> GetbyIdAsync(int id);
        Task<(bool IsSucceeded, string ErrorMsg)> BorrowAsync(int userid, int bookid, DateTime returnDate);
        Task<(bool IsSucceeded, string ErrorMsg)> ReturnAsync(int userId, int bookId);
        Task<(bool IsSucceeded, string ErrorMsg)> AddAsync(string name, string author, decimal price);
        Task<(bool IsSucceeded, string ErrorMsg)> UpdateAsync(int id, string name, string author, decimal price);
        Task<(bool IsSucceeded, string ErrorMsg)> DeleteAsync(int id);
        Task<(bool IsSucceeded, string ErrorMsg)> AddUserAsync(string name, string phone, string address);
        Task<(bool IsSucceeded, string ErrorMsg)> AddBlacklistAsync(int userId);
    }
}
