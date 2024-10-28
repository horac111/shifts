using Shifts.BL.Data.Dto;
using Shifts.BL.Data.DTO;

namespace Shifts.BL.Abstract.Services;
public interface IManageUsersService
{
    Task<string> CreateUser(UserDto userDto);
    IAsyncEnumerable<string> CreatePassword(CreatePasswordDto dto);
    IAsyncEnumerable<string> ChangePassword(string oldPassword, string newPassword);
    Task<string> SignIn(string login, string password);
}
