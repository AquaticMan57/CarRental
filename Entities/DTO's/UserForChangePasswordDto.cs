using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class UserForChangePasswordDto : IDto
    {
        public int Id { get; set; }
        public string PasswordToChange { get; set; }
        public string Password { get; set; }

    }
}
