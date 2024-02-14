using AutoMapper;
using Business_Access_Layer.Interface.User;
using Data_Access_Layer.Data;
using Data_Access_Layer.Models.UserModel;
using DTO_Layer.DTOsModels.UserModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if (IsUserValid(user, out string errorMessage))
                {
                    // Check for duplicate username or email
                    if (IsDuplicateUser(user.Name, user.Email))
                    {
                        return response = "Name or Email already exists.";
                    }

                    // Additional validation for email format
                    if (!IsValidEmail(user.Email))
                    {
                        return response = "Invalid Email format.";
                    }

                    // Additional validation for password
                    if (!IsPasswordValid(user.Password))
                    {
                        return response = "Invalid Password format. Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.";
                    }

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
                else
                {
                    response = errorMessage; // Assuming you have an error message from the validation
                    return response;
                }

            }
            catch
            {
                response = "Failed";
                return response;
            }
        }

        public bool IsUserValid(UserRegDTO user, out string errorMessage)
        {
            {
                errorMessage = "";

                // Check for missing required fields
                if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.Password))
                {
                    errorMessage = "Username, Email, and Password are required.";
                    return false;
                }

                // Check for valid email format
                if (!IsValidEmail(user.Email))
                {
                    errorMessage = "Invalid Email format.";
                    return false;
                }

                // Check for strong password
                if (user.Password.Length < 8 || !ContainsUppercase(user.Password) || !ContainsLowercase(user.Password) || !ContainsDigit(user.Password))
                {
                    errorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.";
                    return false;
                }

                // Additional validation logic can be added here

                return true;
            }
        }

        public bool IsDuplicateUser(string name, string email)
        {
            var data = _context.Users.Where(u => u.Name == name || u.Email == email);
            bool isDuplicate = data != null && data.Any();
            return isDuplicate;

            // var data = _context.Users.Where(u => u.Name == name || u.Email == email);
            // return data.Any();
        }

        public bool IsValidEmail(string email)
        {
            {
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == email;
                }
                catch
                {
                    return false;
                }
            }
        }

        //  These service will check : "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit."
        public bool ContainsUppercase(string input)
        {
            {
                foreach (char c in input)
                {
                    if (char.IsUpper(c))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public bool ContainsLowercase(string input)
        {
            {
                foreach (char c in input)
                {
                    if (char.IsLower(c))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool ContainsDigit(string input)
        {
            {
                foreach (char c in input)
                {
                    if (char.IsDigit(c))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsPasswordValid(string password)
        {
            // Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one digit.
            return password.Length >= 8 && ContainsUppercase(password) && ContainsLowercase(password) && ContainsDigit(password);
        }

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
                    response.Message = "Invalid Email or Password";
                }
                return response;
            }
            catch (Exception)
            {
                response.Message = "Failed";
                return response;
            }
        }
    }
}
