using Besho.DAL.DTO.Requests;
using Besho.DAL.DTO.Responses;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Besho.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
       Task <UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest);

    }
}
