using System;
using System.Threading.Tasks;
using AspNetCoreToDo.Data;
using AspNetCoreToDo.Models;
using AspNetCoreToDo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace AspNetCoreToDo.UnitTests
{
    public class ToDoItemServiceShould
    {
        [Fact]
        public async Task AddNewItemAsIncompleteWithDueDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);
                var fakeUser = new IdentityUser
                {
                    Id = "fake-000",
                    UserName = "fake@test.com"
                };

                await service.AddItemAsync(new ToDoItem
                {
                    Title = "Testing?"
                }, fakeUser);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var itemsInDatabase = await context.Items.CountAsync();
                Assert.Equal(1, itemsInDatabase);

                var item = await context.Items.FirstAsync();
                Assert.Equal("Testing?", item.Title);
                Assert.False(item.IsDone);
                Assert.True(item.DueAt > DateTimeOffset.Now);
            }
        }

        [Fact]
        public async Task MarkDoneAnItem()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_MarkDone").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);
                var fakeUser = new IdentityUser
                {
                    Id = "fake-000",
                    UserName = "fake@test.com"
                };
            
                var item = new ToDoItem
                {
                    Title = "Testing?"
                };                
                await service.AddItemAsync(item, fakeUser);
                var itemId = item.Id;

                await service.MarkDoneAsync(itemId, fakeUser);

            }

            using (var context = new ApplicationDbContext(options))
            {
                var itemsInDatabase = await context.Items.CountAsync();
                Assert.Equal(1, itemsInDatabase);

                var item = await context.Items.FirstAsync();
                Assert.True(item.IsDone);

            }
        }

        [Fact]
        public async Task MarkDoneAnItemCorrectID()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_MarkDoneID").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);
                var fakeUser = new IdentityUser
                {
                    Id = "fake-000",
                    UserName = "fake@test.com"
                };
            
                var item = new ToDoItem
                {
                    Title = "Testing?"
                };                
                await service.AddItemAsync(item, fakeUser);

                var itemId = System.Guid.NewGuid();

                await service.MarkDoneAsync(itemId, fakeUser);

            }

            using (var context = new ApplicationDbContext(options))
            {
                var itemsInDatabase = await context.Items.CountAsync();
                Assert.Equal(1, itemsInDatabase);

                var item = await context.Items.FirstAsync();
                Assert.False(item.IsDone);

            }
        }

        [Fact]
        public async Task GetIncompleteItems()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_GetIncompleteItems").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);
                var fakeUser1 = new IdentityUser
                {
                    Id = "fake-000",
                    UserName = "fake@test.com"
                };
                var fakeUser2 = new IdentityUser
                {
                    Id = "fake-001",
                    UserName = "fake2@test.com"
                };
                var item1 = new ToDoItem
                {
                    Title = "Testing?"
                };  
                var item2 = new ToDoItem
                {
                    Title = "Testing"
                }; 
                await service.AddItemAsync(item1, fakeUser1);
                await service.AddItemAsync(item2, fakeUser2);

                

            }

            using (var context = new ApplicationDbContext(options))
            {
                var fakeUser1 = new IdentityUser
                {
                    Id = "fake-000",
                    UserName = "fake@test.com"
                };
                var fakeUser2 = new IdentityUser
                {
                    Id = "fake-001",
                    UserName = "fake2@test.com"
                };
                var service = new ToDoItemService(context);

                var itemsInDatabase = await context.Items.CountAsync();
                Assert.Equal(2, itemsInDatabase);

                var items = await service.GetIncompleteItemsAsync(fakeUser1);
                Assert.Single(items);

                var items2 = await service.GetIncompleteItemsAsync(fakeUser2);
                Assert.Single(items2);

            }
        }

    }
}