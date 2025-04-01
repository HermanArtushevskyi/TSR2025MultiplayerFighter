using Microsoft.AspNetCore.Mvc;
using TSR2025Backend.Data;

namespace TSR2025Backend.Controllers;

[ApiController]
[Route("[controller]")]
public class LobbyController : ControllerBase
{
    [HttpGet("get")]
    public ActionResult<IEnumerable<Lobby>> GetAllLobbies()
    {
        List<LobbyDTO> lobbies = new();
        foreach (Lobby lobby in ApplicationContext.Instance.Lobbies)
        {
            lobbies.Add(new LobbyDTO
            {
                Name = lobby.Name,
                FirstUserLogin = lobby.FirstUserLogin,
                SecondUserLogin = lobby.SecondUserLogin,
                Address = lobby.Address
            });
        }
        
        return Ok(lobbies);
    }
    
    [HttpGet("getByName")]
    public ActionResult<LobbyDTO> GetLobbyByName(string name)
    {
        Lobby lobby = ApplicationContext.Instance.Lobbies.FirstOrDefault(lobby => lobby.Name == name);
        if (lobby == null)
        {
            return BadRequest("Lobby not found");
        }
        
        return Ok(new LobbyDTO
        {
            Name = lobby.Name,
            FirstUserLogin = lobby.FirstUserLogin,
            SecondUserLogin = lobby.SecondUserLogin,
            Address = lobby.Address
        });
    }
    
    [HttpGet("create")]
    public ActionResult<string> CreateLobby(string name, string firstUserLogin, string address)
    {
        Console.WriteLine($"Creating lobby {name} {firstUserLogin} {address}");;
        Lobby lobby = new Lobby
        {
            Name = name,
            FirstUserLogin = firstUserLogin,
            SecondUserLogin = "",
            Address = address
        };
        
        ApplicationContext.Instance.Lobbies.Add(lobby);
        ApplicationContext.Instance.SaveChanges();
        return Ok("Lobby created");
    }
    
    [HttpGet("join")]
    public ActionResult<string> JoinLobby(string name, string secondUserLogin)
    {
        Lobby lobby = ApplicationContext.Instance.Lobbies.FirstOrDefault(lobby => lobby.Name == name);
        if (lobby == null)
        {
            return BadRequest("Lobby not found");
        }
        
        lobby.SecondUserLogin = secondUserLogin;
        ApplicationContext.Instance.SaveChanges();
        return Ok(lobby.Address);
    }
    
    [HttpGet("leave")]
    public ActionResult<string> LeaveLobby(string userLogin)
    {
        Lobby lobby = ApplicationContext.Instance.Lobbies.FirstOrDefault(lobby => lobby.SecondUserLogin == userLogin || lobby.FirstUserLogin == userLogin);
        if (lobby == null)
        {
            return BadRequest("Lobby not found");
        }
        
        if (lobby.FirstUserLogin == userLogin)
        {
            DeleteLobby(lobby.Name);
            return Ok("Lobby deleted");
        }
        else if (lobby.SecondUserLogin == userLogin)
        {
            lobby.SecondUserLogin = null;
        }
        else
        {
            return BadRequest("User not found in lobby");
        }
        
        ApplicationContext.Instance.SaveChanges();
        return Ok("User left lobby");
    }
    
    [HttpGet("delete")]
    public ActionResult<string> DeleteLobby(string name)
    {
        Lobby lobby = ApplicationContext.Instance.Lobbies.FirstOrDefault(lobby => lobby.Name == name);
        if (lobby == null)
        {
            return BadRequest("Lobby not found");
        }
        
        ApplicationContext.Instance.Lobbies.Remove(lobby);
        ApplicationContext.Instance.SaveChanges();
        return Ok("Lobby deleted");
    }
}