using AutoMapper;
using Business_Access_Layer.Interface.User;
using Data_Access_Layer.Data;
using Data_Access_Layer.Migrations;
using Data_Access_Layer.Models.CategoryModel;
using Data_Access_Layer.Models.UserModel;
using DTO_Layer.DTOsModels.UserModelDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Service.User
{
    public class UserService : UserInterface
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string Registration(UserRegDTO user)
        {
            var response = "";

            try
            {
                // Valid Registration
                //if (IsUserValid(user, out string errorMessage))
                {
                    // Check for duplicate username or email
                    //if (IsDuplicateUser(user.Name, user.Email))
                    //{
                    //    return response = "Name or Email already exists.";
                    //}

                    // Additional validation for email format
                    //if (!IsValidEmail(user.Email))
                    //{
                    //    return response = "Invalid Email format.";
                    //}

                    // Additional validation for password
                    //if (!IsPasswordValid(user.Password))
                    //{
                    //    return response = "Invalid Password format. Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.";
                    //}

                    user.Name = user.Name.Trim();
                    user.Email = user.Email.Trim();
                    user.Password = Encryption.Encrypt(user.Password);


                    var userEntity = _mapper.Map<UserApi>(user); // Assuming you have AutoMapper configured
                    userEntity.Id = Guid.NewGuid(); // Generate a new Guid
                    _context.Users.Add(userEntity);
                    _context.SaveChanges();

                    response = "Success";
                    return response;
                }
            }
            catch
            {
                response = "Failed";
                return response;
            }
        }

        //public bool IsUserValid(UserRegDTO user, out string errorMessage)
        //{
        //    {
        //        errorMessage = "";

        //        // Check for missing required fields
        //        if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
        //        {
        //            errorMessage = "Username, Email, and Password are required.";
        //            return false;
        //        }

        //        // Check for valid email format
        //        if (!IsValidEmail(user.Email))
        //        {
        //            errorMessage = "Invalid Email format.";
        //            return false;
        //        }

        //        // Check for strong password
        //        if (user.Password.Length < 8 || !ContainsUppercase(user.Password) || !ContainsLowercase(user.Password) || !ContainsDigit(user.Password))
        //        {
        //            errorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.";
        //            return false;
        //        }

        //        // Additional validation logic can be added here

        //        return true;
        //    }
        //}

        //public bool IsDuplicateUser(string name, string email)
        //{
        //    var data = _context.Users.Where(u => u.Name == name || u.Email == email);
        //    bool isDuplicate = data != null && data.Any();
        //    return isDuplicate;

        //    // var data = _context.Users.Where(u => u.Name == name || u.Email == email);
        //    // return data.Any();
        //}

        //public bool IsValidEmail(string email)
        //{
        //    {
        //        try
        //        {
        //            var addr = new System.Net.Mail.MailAddress(email);
        //            return addr.Address == email;
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }
        //}

        //  These service will check : "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit."
        //public bool ContainsUppercase(string input)
        //{
        //    {
        //        foreach (char c in input)
        //        {
        //            if (char.IsUpper(c))
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //}
        //public bool ContainsLowercase(string input)
        //{
        //    {
        //        foreach (char c in input)
        //        {
        //            if (char.IsLower(c))
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //}

        //public bool ContainsDigit(string input)
        //{
        //    {
        //        foreach (char c in input)
        //        {
        //            if (char.IsDigit(c))
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //}

        //public bool IsPasswordValid(string password)
        //{
        //    // Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.
        //    return password.Length >= 8 && ContainsUppercase(password) && ContainsLowercase(password) && ContainsDigit(password);
        //}

        public UserResponseDTO Login(string email, string password)
        {
            var response = new UserResponseDTO();

            try
            {
                email = email.Trim();
                password = Encryption.Encrypt(password);

                var data = _context.Users.Where(a => a.Email.ToLower() == email.ToLower() && a.Password == password).FirstOrDefault();
                if (data != null)
                {
                    response.Message = "Success";
                    response.Id = data.Id;
                }
                else
                {
                    response.Message = "Invalid Credentials";
                }
                return response;
            }
            catch (Exception)
            {
                response.Message = "Failed";
                return response;
            }
        }

        public List<UserApi> GetUsers()
        {
            var Users = _context.Users.ToList();
            // Perform mapping using AutoMapper
            var data = _mapper.Map<List<UserApi>>(Users);
            return data;
        }

        public UserApi GetUserData(Guid id)
        {
            var data = _context.Users.FirstOrDefault(a => a.Id == id);
            return data;
        }

        public string UserEdit(UserEditDTO user)
        {
            try
            {
                var existingUser = _context.Users.FirstOrDefault(a => a.Id == user.Id);

                if (existingUser == null)
                {
                    return "User not found";
                }

                // Only update properties if they're not null or empty
                if (!string.IsNullOrEmpty(user.Name))
                    existingUser.Name = user.Name.Trim();

                if (!string.IsNullOrEmpty(user.Email))
                    existingUser.Email = user.Email.Trim();

                if (!string.IsNullOrEmpty(user.PhoneNumber))
                    existingUser.PhoneNumber = user.PhoneNumber;

                _context.Users.Update(existingUser);
                _context.SaveChanges();

                return "Success";
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Exception: {ex.Message}");
                return "Failed";
            }
        }

        public string UserDelete(Guid id)
        {
            var response = "";

            try
            {
                var user = _context.Users.Any(a => a.Id == id);

                if(user)
                {
                    //Find the user by Id
                    var userdelete = _context.Users.Find(id);

                    if (userdelete != null)
                    {
                        _context.Users.Remove(userdelete);
                        _context.SaveChanges();

                        response = "Success";
                    }
                    else
                    {
                        response = "Failed To Delete User";
                    }
                }
                return response;
            }
            catch
            {
                return "Failed";
            }
        }

        public async Task<string> ForgotPassword(string email)
        {
            var response = "";

            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

                if (user != null)
                {
                    // Generate a unique token (you can use Guid.NewGuid() for simplicity)
                    string resetToken = Guid.NewGuid().ToString();

                    // Store the token in the database with a timestamp for expiration
                    user.ResetToken = resetToken;
                    user.ResetTokenExpiration = DateTime.UtcNow.AddHours(1); // Set expiration time

                    await _context.SaveChangesAsync();

                    // Send an email with the password reset link
                    await SendPasswordResetEmail(email, resetToken);

                    return response = "Password reset link sent successfully";
                }
                else
                {
                    return response = "User not found";
                }
            }
            catch (Exception ex)
            {
                return "Failed to send password reset link";
            }
        }
        public async Task<string> ResetPassword(string token, string newPassword)
        {
            try
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.ResetToken == token);

                if (user != null)
                {
                    if (user.ResetTokenExpiration <= DateTime.UtcNow)
                    {
                        // Log or handle the case where the token is expired
                        return "Token expired";
                    }

                    // Reset the password
                    user.Password = Encryption.Encrypt(newPassword);
                    user.ResetToken = "-";
                    user.ResetTokenExpiration = null;

                    await _context.SaveChangesAsync();

                    return "Password reset successfully";
                }
                else
                {
                    // Log or handle the case where the user is not found or any other unexpected scenarios
                    return "Invalid token or user not found";
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                return "Failed to reset password";
            }
        }
        public async Task SendPasswordResetEmail(string toEmail, string resetToken)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("test25943026@gmail.com", "inotbtixswttcxlm"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("test25943026@gmail.com"),
                    Subject = "Password Reset",
                    Body = $@"
                        <html>
                        <head>
                            <style>
                                body {{
                                    font-family: 'Arial', sans-serif;
                                    background-color: #f4f4f4;
                                }}
                                .container {{
                                    max-width: 600px;
                                    margin: 0 auto;
                                    padding: 20px;
                                    background-color: #ffffff;
                                    border: 1px solid #dddddd;
                                    border-radius: 5px;
                                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                }}
                                .reset-link {{
                                    display: block;
                                    padding: 10px;
                                    background-color: blue;
                                    color: #f4f4f4;
                                    text-align: center;
                                    text-decoration: none;
                                    border-radius: 5px;
                                }}

                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <p>Hello,</p>
                                <p>Click the following link to reset your password:</p>
                                <center><a href='https://www.example.com/password-reset?token={resetToken}'><button  class='reset-link'>Reset Password</button></a></center>
         
                            </div>
                                <p style='Color:#D3D3D3'>This link will expire within 1 hour</p>
                        </body>
                        </html>",
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(toEmail);


                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw new Exception("Failed to send email", ex);
            }
        }

    }
}
