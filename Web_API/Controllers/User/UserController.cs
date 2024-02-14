using Business_Access_Layer.Interface.User;
using Business_Access_Layer.Service.User;
using DTO_Layer.DTOsModels.UserModelDTO;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserInterface _userInterface;

        public UserController(UserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpPost("register")]
        public IActionResult User_Register(UserRegDTO user)
        {
            try
            {
                var response = _userInterface.Registration(user);
                if (response == "Success")
                {
                    return Json(new { message = "Success" });
                }
                else
                {
                    // If registration failed, return BadRequest with a message
                    return BadRequest(new { message = "Registration failed" });
                }
            }
            catch (Exception)
            {
                // Log the exception if needed
                // logger.LogError(ex, "An error occurred during user registration.");

                // Return a generic error message
                return StatusCode(500, new { message = "An Error occurred while processing your request. Please try again later." });
            }
        }

        [HttpPost("login")]
        public IActionResult User_Login(UserLogDTO user)
        {
            var response = new UserResponseDTO();

            if (user == null || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest(response.Message = "Email and password are required.");
            }

            try
            {
                response = _userInterface.Login(user.Email, user.Password);

                if (response.Message == "Success")
                {
                    return Json(response.Message = "Success");
                }
                else
                {
                    // If login failed, return UnauthorizedRequest with a message
                    return Unauthorized(response.Message = "Invalid credentials");
                }
            }
            catch (Exception)
            {
                // Log the exception if needed
                // logger.LogError(ex, "An error occurred during user login.");

                // Return a generic error message
                return StatusCode(500, new { mesaage = "An Error occurred while processing your request. Please try again later." });
            }
        }
    }
}
