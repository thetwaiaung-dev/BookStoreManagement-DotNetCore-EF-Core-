using BookManagement.Models;
using BookManagement.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookManagement.Services
{
    public class AuthorService : IAuthorRepo
    {
        private readonly BookDbContext _dbContent;

        public AuthorService(BookDbContext dbContext)
        {
            _dbContent = dbContext;
        }

        public int Create(BookAuthor entity)
        {
            _dbContent.Author.Add(entity);
            return _dbContent.SaveChanges();
        }

        public int Delete(BookAuthor entity)
        {
            throw new System.NotImplementedException();
        }

        public List<BookAuthor> GetAll()
        {
            return _dbContent.Author.OrderByDescending(x => x.Author_Id).ToList();
        }

        public AuthorResponseModel GetAll(string? searchValue, int? pageNo, int? pageSize, long? categoryId)
        {
            var books = from b in _dbContent.Book select b;

            if (categoryId > 0)
            {
                books = books.Where(x => x.Category_Id == categoryId);
            }

            var authors = books.Select(x => x.Author).Distinct();

            if (string.IsNullOrEmpty(searchValue))
            {
                authors = authors.Where(x => x.Author_Name.Contains(searchValue));
            }

            var items = authors.OrderByDescending(x => x.Author_Id)
                                .Skip((int)((pageNo - 1) * pageSize))
                                .Take((int)pageSize).ToList();

            int count = authors.Count();
            int totalPages = (int)((count + pageSize - 1) / pageSize);

            AuthorResponseModel model = new AuthorResponseModel()
            {
                AuthorCount = count,
                TotalPages = totalPages,
                Authors = items
            };
            return model;
        }

        public BookAuthor GetById(long id)
        {
            BookAuthor author = _dbContent.Author.FirstOrDefault(x => x.Author_Id == id);
            return author;
        }

        public int Update()
        {
            return _dbContent.SaveChanges();
        }

        public bool IsExist(int id)
        {
            bool isExist = _dbContent.Author.AsNoTracking().Any(x => x.Author_Id == id);
            return isExist;
        }

        public void SaveImage(string photoFile, string path)
        {
            if (photoFile != null && photoFile != string.Empty)
            {
                var base64array = Convert.FromBase64String(photoFile);

                File.WriteAllBytes(path, base64array);
            }
        }

        public string GetPhotoName(string photo)
        {
            string[] strings = photo.Split("_");
            return strings[strings.Length - 1];
        }

        public string GetAuthorFolder(string photoName)
        {
            string folder = "photos/author/";
            folder += Guid.NewGuid().ToString() + "_" + photoName;

            return folder;
        }

        public void RemoveAuthorFolder(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public int Update(BookAuthor entity)
        {
            throw new NotImplementedException();
        }
    }
}
