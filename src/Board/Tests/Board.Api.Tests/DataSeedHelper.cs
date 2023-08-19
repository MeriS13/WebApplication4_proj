using System;
using Board.Domain.Posts;
using Board.Domain.Categories;
using Board.Infrastructure.DataAccess;
using Board.Infrastructure.DataAccess;
using Xunit;
using Board.Domain.Accounts;

namespace Board.Api.Tests
{
    public static class DataSeedHelper
    {
        public static Guid TestParentCategoryId { get; set; }
        public static Guid TestPostId { get; set; }
        public static Guid TestCategoryId { get; set; }
        public static Guid TestAccountUserId { get; set; }

        public static void InitializeDbForTests(BoardDbContext db)
        {
            var testAccountUser = new Account
            {
                Email = "user@test.com",
                Name = "User",
                Login = "User",
                Password = "useruser",
                Created = DateTime.UtcNow,
            };
            db.Add(testAccountUser);


            var testParentCategory = new ParentCategory
            {
                Name = "test_ParCat_1",
            };
            db.Add(testParentCategory);


            var testCategory = new Category
            {
                ParentId = testParentCategory.Id,
                Name = "test_cat_1",
            };
            db.Add(testCategory);


            var Post = new Post
            {
                Name = "test_post_name1",
                Description = "test_desc1",
                CreationDate = DateTime.UtcNow,
                CategoryId = testCategory.Id,
                AccountId = testAccountUser.Id,
                IsFavorite = true,
                
            };
            db.Add(Post);


            db.SaveChanges();

            TestCategoryId = testCategory.Id;
            TestPostId = Post.Id;
            TestParentCategoryId = testParentCategory.Id;
            TestAccountUserId = testAccountUser.Id;
        }
    }
}