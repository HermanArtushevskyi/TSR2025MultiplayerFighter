using Microsoft.AspNetCore.Mvc;
using TSR2025Backend.Data;

namespace TSR2025Backend.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost("~/register")]
    public ActionResult<string> Register(string login, string password)
    {
        bool userWithLoginExists = false;
        userWithLoginExists = ApplicationContext.Instance.Users.Any(user => user.Login == login);
        if (userWithLoginExists)
        {
            return BadRequest("User with this login already exists");
        }
        
        User user = new User
        {
            Login = login,
            Password = password
        };
        
        ApplicationContext.Instance.Users.Add(user);
        ApplicationContext.Instance.SaveChanges();
        string guid = Guid.NewGuid().ToString();
        ApplicationContext.Instance.AuthenticationCodes.Add(new AuthenticationCode
        {
            UserId = user.Id,
            Value = guid
        });
        ApplicationContext.Instance.SaveChanges();
        return Ok(guid);
    }

    [HttpGet("~/login")]
    public ActionResult<string> Login(string login, string password)
    {
        User user = ApplicationContext.Instance.Users.FirstOrDefault(user => user.Login == login && user.Password == password);
        if (user == null)
        {
            return BadRequest("Invalid login or password");
        }
        
        string guid = Guid.NewGuid().ToString();
        ApplicationContext.Instance.AuthenticationCodes.Add(new AuthenticationCode
        {
            UserId = user.Id,
            Value = guid
        });
        ApplicationContext.Instance.SaveChanges();
        return Ok(guid);
    }

    [HttpGet("~/getUser")]
    public ActionResult<User> GetUserByCode(string authenticationCode)
    {
        AuthenticationCode code = ApplicationContext.Instance.AuthenticationCodes.FirstOrDefault(code => code.Value == authenticationCode);
        if (code == null)
        {
            return BadRequest("Invalid authentication code");
        }

        User user = ApplicationContext.Instance.Users.FirstOrDefault(user => user.Id == code.UserId);
        return Ok(user);
    }

    [HttpPost("~/logout")]
    public void LogOut(string key)
    {
        AuthenticationCode code = ApplicationContext.Instance.AuthenticationCodes.FirstOrDefault(code => code.Value == key);
        if (code == null)
        {
            return;
        }

        ApplicationContext.Instance.AuthenticationCodes.Remove(code);
        ApplicationContext.Instance.SaveChanges();
    }
}