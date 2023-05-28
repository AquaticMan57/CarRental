using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class UserDetailDto : IDto
    {
        public int UserId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyMail { get; set; }
        public bool Status { get; set; }
    }
}
