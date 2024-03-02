using Data_Access_Layer.Models.UserModel;
using DTO_Layer.DTOsModels.UserModelDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Access_Layer.Interface.User
{
    public interface UserInterface
    {
        // Define an interface for registration logic, then use it in your controller to handle registration requests.
        public string Registration(UserRegDTO user);

        //This is how you would check whether a user is valid.
        //public bool IsUserValid(UserRegDTO user, out string errorMessage);

        //You can check if anyone's already signed up with the same info. 
        //public bool IsDuplicateUser(string name, string email);

        //This is how the system determines whether an email address is valid.
        //public bool IsValidEmail(string email);

        //The system checks if a character contains characters from the uppercase English alphabet (a-z).
        //public bool ContainsUppercase(string input);

        //The system checks if a character contains characters from the lowercase English alphabet (a-z).

        //public bool ContainsLowercase(string input);

        // The system checks if a string contains at least one digit (0-9, including Roman numerals).
        //public bool ContainsDigit(string input);


        //public bool IsPasswordValid(string password);

        public UserResponseDTO Login(string email, string password);

        public List<UserApi> GetUsers();

        public UserApi GetUserData(Guid id);

        public string UserDelete(Guid id);
    }
}
