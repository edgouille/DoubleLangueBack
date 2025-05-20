using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoubleLangue.Domain.Dto;

public class UserDto
{
    public string Id { get; set; }// guid to generate
    public string UserName { get; set; } 
    public string Email { get; set; } // unique
    public string Role { get; set; } // user, admin
    public string Password { get; set; }// hashed
}