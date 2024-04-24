

using Microsoft.AspNetCore.Mvc;

[Route("[Action]")]
[ApiController]

public class AccountController:Controller
{

    private readonly JwtTokenHandler  _jwtTokenHandler;

    public AccountController(JwtTokenHandler jwtTokenHandler)
    {
        _jwtTokenHandler = jwtTokenHandler;
    }

    [HttpPost]
    public  ActionResult<AutanticationResponse> Login([FromBody] AutanticationRequest request)
    {
        //null
        if (request == null)
        {
            return Unauthorized();
        }
        //not find user
        if (_jwtTokenHandler.UserAccounts.FirstOrDefault(x => x.UserName == request.UserName && x.Password == request.Password) == null)
        {
            return Unauthorized();
        }

        return _jwtTokenHandler.GenerateToken(request);
    }


    
}