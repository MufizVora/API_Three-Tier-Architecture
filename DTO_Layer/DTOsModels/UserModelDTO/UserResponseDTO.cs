using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_Layer.DTOsModels.UserModelDTO
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }

        public string? Message { get; set; }
    }
}
