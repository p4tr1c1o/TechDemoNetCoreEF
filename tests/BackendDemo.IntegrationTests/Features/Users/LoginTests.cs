﻿using System;
using System.Threading.Tasks;
using BackendDemo.Domain;
using BackendDemo.Features.Users;
using BackendDemo.Infrastructure.Security;
using Xunit;

namespace BackendDemo.IntegrationTests.Features.Users
{
    public class LoginTests : SliceFixture
    {
        [Fact]
        public async Task Expect_Login()
        {
            var salt = Guid.NewGuid().ToByteArray();
            var person = new Person
            {
                Username = "username",
                Email = "email",
                Hash = await new PasswordHasher().Hash("password", salt),
                Salt = salt
            };
            await InsertAsync(person);

            var command = new Login.Command()
            {
                User = new Login.UserData()
                {
                    Email = "email",
                    Password = "password"
                }
            };

            var user = await SendAsync(command);

            Assert.NotNull(user?.User);
            Assert.Equal(user.User.Email, command.User.Email);
            Assert.Equal("username", user.User.Username);
            Assert.NotNull(user.User.Token);
        }
    }
}