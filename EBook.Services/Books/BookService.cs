using EBook.Core.Entites;
using EBook.Core.Interfaces;
using EBook.DTOs.Books;
using EBook.DTOs.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EBook.Services.Books
{
    public class BookService : IBookService
    {

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<UserXBook> _userXBookRepository;
        private readonly IRepository<Blacklist> _blackListRepository;

        public BookService(IRepository<User> userRepository,
            IRepository<Book> bookRepository,
            IRepository<UserXBook> userXBookRepository,
            IRepository<Blacklist> blackListRepository)
        {
            _userRepository = userRepository;
            _bookRepository = bookRepository;
            _userXBookRepository = userXBookRepository;
            _blackListRepository = blackListRepository;
        }


        //获取全部书本详情
        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            if (books != null)
            {
                var bookDtos = new List<BookDto>();

                foreach (var book in books)
                {
                    bookDtos.Add(book.ToDto());
                }

                return bookDtos;
            }
            return null;
        }

        /// <summary>
        /// 还书
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<(bool IsSucceeded, string ErrorMsg)> ReturnAsync(int userId, int bookId)
        {
            bool isSucceeded = false; string errorMsg = string.Empty;

            //1. 查询UserXBook表中的数据
            var userXBook = await _userXBookRepository.SingleOrDefaultAsync(x => x.UserID == userId && x.BookID == bookId);

            //2. 查询blackList中的数据
            var blackList = await _blackListRepository.SingleOrDefaultAsync(u => u.UserId == userId);

            //3. 判断是否存在该记录
            if (userXBook != null)
            {
                //4. 获取该记录状态是否为未借书
                if (userXBook.State == State.Return)
                {
                    errorMsg = "此书已还";
                }
                else
                {
                    //5.如果超过期限还书
                    if (userXBook.ReturnDate < DateTime.Now)
                    {
                        //6. 判断blackList表是否存在数据
                        if (blackList != null)
                        {
                            //6.1. 更新数据
                            userXBook.RealReturnDate = DateTime.Now;
                            userXBook.State = State.Return;
 
                            await _userXBookRepository.UpdateAsync(userXBook);

                            blackList.Count++;

                            await _blackListRepository.UpdateAsync(blackList);
                        }
                        else
                        {
                            //6.2.如果在blackList表没有存在数据
                            userXBook.RealReturnDate = DateTime.Now;
                            userXBook.State = State.Return;

                            await _userXBookRepository.UpdateAsync(userXBook);

                            //6.3. 如果为空则添加一条数据在blackList
                            blackList = new Blacklist()
                            {
                                UserId = userId,
                                Count = 1
                            };

                            await _blackListRepository.InsertAsync(blackList);
                        }
                    }
                    else
                    {
                        //7.如果在期限内还书
                        userXBook.RealReturnDate = DateTime.Now;
                        userXBook.State = State.Return;

                        await _userXBookRepository.UpdateAsync(userXBook);

                        //7.1 判断该用户是否存在黑名单里
                        if (blackList != null)
                        {
                            blackList.Count--;

                            if (blackList.Count == 0)
                            {
                                await _blackListRepository.DeleteAsync(blackList);
                            }
                        }
                    }
                }
            }
            else
            {
                errorMsg = "非法操作";
            }

            return (isSucceeded, errorMsg);
        }

        /// <summary>
        /// 借书
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bookId"></param>
        /// <param name="returnDate"></param>
        /// <returns></returns>
        public async Task<(bool IsSucceeded, string ErrorMsg)> BorrowAsync(int userId, int bookId, DateTime returnDate)
        {
            bool isSucceeded = false; string errorMsg = string.Empty;

            if (returnDate < DateTime.Today)
            {
                errorMsg = "请输入正确的还书时间";

                return (isSucceeded, errorMsg);
            }

            //1. 用book表查询该书
            var book = await _bookRepository.SingleOrDefaultAsync(u => u.ID == userId);

            //2. 从用户表查询该用户
            var user = await _userRepository.SingleOrDefaultAsync(u => u.ID == userId);

            //3. 判断书籍和用户是否存在
            if (book != null && user != null)
            {
                //4. 在blackList中是否存在该记录
                var blackList = await _blackListRepository.SingleOrDefaultAsync(e => e.UserId == userId);

                if (blackList?.Count < 10)
                {
                    //5. 在UserXBook表中查询是否有该记录
                    var userXBook = await _userXBookRepository.SingleOrDefaultAsync(u => u.UserID == userId && u.BookID == bookId);

                    //6. 判断是否已经借书
                    if (userXBook == null || userXBook?.State == State.Return)
                    {
                        if (userXBook == null)
                        {
                            //7. 如果不存在则添加一条新的借书记录
                            userXBook = new UserXBook
                            {
                                BookID = bookId,
                                UserID = userId,
                                BorrowDate = DateTime.Now,
                                ReturnDate = returnDate,
                                State = State.Borrow
                            };

                            await _userXBookRepository.InsertAsync(userXBook);
                        }
                        else
                        {
                            //11. 如果存在则直接更新借书跟还书的时间
                            userXBook.BorrowDate = DateTime.Now;
                            userXBook.ReturnDate = returnDate;
                            userXBook.State = State.Borrow;

                            await _userXBookRepository.UpdateAsync(userXBook);

                        }

                        isSucceeded = true;
                    }
                    else
                    {
                        errorMsg = "书籍已借出";
                    }
                }
                else
                {
                    //5. 在UserXBook表中查询是否有该记录
                    var userXBook = await _userXBookRepository.SingleOrDefaultAsync(u => u.UserID == userId && u.BookID == bookId);

                    //6. 判断是否已经借书
                    if (userXBook == null || userXBook?.State == State.Return)
                    {
                        if (userXBook == null)
                        {
                            //7. 如果不存在则添加一条新的借书记录
                            userXBook = new UserXBook
                            {
                                BookID = bookId,
                                UserID = userId,
                                BorrowDate = DateTime.Now,
                                ReturnDate = returnDate,
                                State = State.Borrow
                            };

                            await _userXBookRepository.InsertAsync(userXBook);
                        }
                        else
                        {
                            //11. 如果存在则直接更新借书跟还书的时间
                            userXBook.BorrowDate = DateTime.Now;
                            userXBook.ReturnDate = returnDate;
                            userXBook.State = State.Borrow;

                            await _userXBookRepository.UpdateAsync(userXBook);

                        }

                        isSucceeded = true;
                    }
                    else
                    {
                        errorMsg = "书籍已借出";
                    }
                }
            }
            else
            {
                errorMsg = "非法借书";
            }

            return (isSucceeded, errorMsg);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<(bool IsSucceeded, string ErrorMsg)> AddAsync(string name, string author, decimal price)
        {
            bool isSucceeded = false; string errorMsg = string.Empty;

            Book book = new Book()
            {
                Name = name,
                Author = author,
                Price = price
            };

            await _bookRepository.InsertAsync(book);

            isSucceeded = true;

            return (isSucceeded, errorMsg);

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(bool IsSucceeded, string ErrorMsg)> DeleteAsync(int id)
        {
            bool isSucceeded = false; string errorMsg = string.Empty;
            var book = await _bookRepository.SingleOrDefaultAsync(u => u.ID == id);
            if (book != null)
            {
                _bookRepository.Delete(book);

                isSucceeded = true;
            }
            else
            {
                errorMsg = "没有数据";
            }

            return (isSucceeded, errorMsg);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BookDto> GetbyIdAsync(int id)
        {
            var book = await _bookRepository.SingleOrDefaultAsync(u => u.ID == id);

            if (book != null)
            {
                return book.ToDto();
            }

            return null;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public async Task<(bool IsSucceeded, string ErrorMsg)> UpdateAsync(int id, string name, string author, decimal price)
        {
            bool isSucceeded = false; string errorMsg = string.Empty;
            Book book = await _bookRepository.SingleOrDefaultAsync(u => u.ID == id);
            if (book != null)
            {
                book.Name = name;
                book.Author = author;
                book.Price = price;

                _bookRepository.Update(book);

                isSucceeded = true;
            }
            else
            {
                errorMsg = "此书不存在";
            }
            return (isSucceeded, errorMsg);
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        public async Task<(bool IsSucceeded, string ErrorMsg)> AddUserAsync(string name, string phone, string address)
        {
            bool isSucceeded = false; string errorMsg = string.Empty;
            User user = new User()
            {
                Name = name,
                Phone = phone,
                Address = address
            };
            await _userRepository.InsertAsync(user);

            return (isSucceeded, errorMsg);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<(bool IsSucceeded, string ErrorMsg)> AddBlacklistAsync(int userId)
        {
            bool isSucceeded = false; string errorMsg = string.Empty;

            //1. 获取userXBook中的数据
            var userXBook = await _userXBookRepository.SingleOrDefaultAsync(u => u.UserID == userId);

            //2. 获取blackList中的数据
            var blackList = await _blackListRepository.SingleOrDefaultAsync(e => e.UserId == userId);

            //3. userXBook表是否有数据
            if (userXBook != null)
            {
                //4. 判断还书日期是否超时
                if (userXBook.ReturnDate < DateTime.Now)
                {
                    //5. 判断blacList表中是否有记录
                    if (blackList != null)
                    {

                        errorMsg = "黑名单已有数据";
                    }
                    else
                    {
                        //6. 如果blackList没有数据则添加一条
                        blackList = new Blacklist()
                        {
                            UserId = userId,

                            Count = 1
                        };

                        _blackListRepository.Insert(blackList);

                    }
                }
                else
                {
                    errorMsg = "非法操作";
                }
            }
            else
            {
                errorMsg = "还不具备条件加入黑名单";
            }

            return (isSucceeded, errorMsg);
        }

        /// <summary>
        /// 获取用户所借的所有书籍
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<BookDto>> GetAllAsync(int userId)
        {
            //查询有没有该用户
            var user = await _userRepository.SingleOrDefaultAsync(u => u.ID == userId);

            if (user != null)
            {
                //查询该用户是否有借书的记录
                var userXBooks = await _userXBookRepository.ToListAsync(u => u.UserID == userId);

                if (userXBooks?.Count > 0)
                {
                    //获取所有借书的bookid
                    var bookIds = userXBooks.Select(b => b.BookID).ToList();

                    //根据bookIds获取所有书籍
                    var books = await _bookRepository.ToListAsync(b => bookIds.Contains(b.ID));

                    if (books?.Count > 0)
                    {
                        var bookDTOs = new List<BookDto>();

                        //遍历books用DTO接收值
                        foreach (var book in books)
                        {
                            bookDTOs.Add(book.ToDto());
                        }

                        return bookDTOs;
                    }
                }
            }

            return null;
        }
    }
}
