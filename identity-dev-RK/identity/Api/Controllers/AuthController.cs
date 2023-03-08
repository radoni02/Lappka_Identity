using Api.Requests;
using Application.Auth.Commands;
using Application.Dto;
using Application.Services;
using Application.User.Commands;
using Convey.CQRS.Commands;
using Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly ICommandDispatcher _command;
        private readonly IUserRequestStorage _userRequestStorage;

        public AuthController(ICommandDispatcher command, IUserRequestStorage userRequestStorage)
        {

            _command = command;
            _userRequestStorage = userRequestStorage;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            var command = new RegisterCommand(request.FirstName, request.LastName, request.EmailAddress,
                request.Password, request.ConfirmPassword);
            await _command.SendAsync(command);
            return Ok();
        }

        //[HttpPost("ShelterRegister")]
        //public async Task<IActionResult> ShelterRegister([FromBody]ShelterRegisterRequest request)  // najpierw notyfikacje i grpc
        //{
        //    var command = new ShelterRegisterCommand(request.OrganizationName, request.Longitude,
        //        request.Latitude, request.NIP, request.KRS, request.PhoneNumber);
        //    await _command.SendAsync(command);
        //    return NoContent();
        //}

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand(request.EmailAddress, request.Password);
            await _command.SendAsync(command);

            var tokens = new TokensDto()
            {
                AccessToken = _userRequestStorage.GetToken(command.AccessTokenCacheId),
                RefreshToken = _userRequestStorage.GetToken(command.RefreshTokenCacheId)
            };
            //HttpUtility.UrlDecode(tokens.ToString());
            return Ok(tokens);

        }

        [HttpPost("Confirm_Email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string emailAddress, [FromQuery] string token)
        {
            var command = new ConfirmEmailCommand(emailAddress, token);
            await _command.SendAsync(command);
            return NoContent();
        }

        [HttpPost("UseToken")]
        public async Task<IActionResult> UseRefreshToken([FromBody] UseRefrehTokenRequest request)
        {
            var command = new UseRefreshTokenCommand(request.AccessToken, request.RefreshToken);
            await _command.SendAsync(command);
            var token = new UseRefreshTokenResultDto(_userRequestStorage.GetToken(command.TokenCatchId));
            return Ok(token);
        }
        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] UseRefrehTokenRequest request)
        {
            var command = new RevokeTokenCommand(request.AccessToken, request.RefreshToken);
            await _command.SendAsync(command);
            return NoContent();
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var command = new ResetPasswordCommand(request.Email);
            await _command.SendAsync(command);
            return NoContent();
        }

        [HttpPost("SetNewPassword/{token}")]
        public async Task<IActionResult> SetNewPassword([FromRoute]string token,SetNewPasswordRequest request)
        {
            var command = new SetNewPasswordCommand(HttpUtility.UrlDecode(token),request.Password,request.ConfirmPassword, request.Email);
            await _command.SendAsync(command);
            return NoContent();
        }
    }
}
