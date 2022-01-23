using ANCIA.Authentication.Application.DTOs;
using ANCIA.Core.Core;

namespace ANCIA.Authentication.Application.Services
{
    public interface IUserApplicationService
    {
        public Task<ProcessResult<IEnumerable<UserDto>>> GetAllAsync();
    }
}
