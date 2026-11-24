using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using swagStore.Business;
using swagStore.Business.Properties;
using SwagStore.Api.Services;
using SwagStore.Core;
using SwagStore.Core.Properties;
using System;
using System.Text.Json;

namespace SwagStore.Api.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UsersController : ControllerBase
    {
        [HttpGet("GetUsers")]
        public IActionResult GetAllUsers()
        {
                List<User> users = JsonFiles.UserDeserializer();

                return Ok(users);
        }

        [HttpPost("login")]
        public IActionResult GetLoginUser([FromBody] UserLoginRequest request)
        {
            List<User> users = JsonFiles.UserDeserializer();

            var password = request.password;
            var hash = Authentication.HashPassword(password);

            User loggedIn = Authentication.LoggingIn(users, request.username, hash);

            if (loggedIn == null)
            {
                return Unauthorized();
            }
            else
            {

                return Ok(loggedIn);
            }
        }


        [HttpPost("PostUser")]
        public IActionResult AddUser(User user)
        {
            List<User> users = JsonFiles.UserDeserializer();

            users.Add(user);

            JsonFiles.UserSerializer(users);

            return Ok(user);
        }

        [HttpPut("{username}")]
        public IActionResult ChangeUser(string username, User changedUser)
        {
            List<User> users = JsonFiles.UserDeserializer();

            User? user = users.FirstOrDefault(n => n.UserName == username);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = changedUser.UserName;
            user.Balance = changedUser.Balance;
            user.Hash = changedUser.Hash;

            JsonFiles.UserSerializer(users);

            return Ok(user);
        }

        [HttpDelete("{username}")]
        public IActionResult DeleteUser(string username)
        {
            List<User> users = JsonFiles.UserDeserializer();

            User? user = users.FirstOrDefault(n =>n.UserName == username);
            if (user == null)
            {
                return NotFound();
            }

            users.Remove(user);

            JsonFiles.UserSerializer(users);
            return NoContent();
        }
    }
}
