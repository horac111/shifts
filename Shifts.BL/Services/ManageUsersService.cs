using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Extensions;
using Shared.Senders;
using Shared.Texts;
using Shifts.BL.Abstract.Services;
using Shifts.BL.Data.Dto;
using Shifts.BL.Data.DTO;
using Shifts.DAL.Models;
using System.Text;

namespace Shifts.BL.Services;
public class ManageUsersService : IManageUsersService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly IMapper mapper;
    private readonly EmailSender emailSender;
    private readonly IHttpContextAccessor contextAccessor;

    public ManageUsersService(UserManager<ApplicationUser> userManager, IMapper mapper,
        EmailSender emailSender, IHttpContextAccessor contextAccessor, SignInManager<ApplicationUser> signInManager)
    {
        this.userManager = userManager;
        this.mapper = mapper;
        this.emailSender = emailSender;
        this.contextAccessor = contextAccessor;
        this.signInManager = signInManager;
    }

    public async Task<string> CreateUser(UserDto userDto)
    {
        var user = mapper.Map<ApplicationUser>(userDto);
        var result = await userManager.CreateAsync(user);
        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(x => x.Description));

        foreach (var role in userDto.Roles)
            await userManager.AddToRoleAsync(user, role.ToString());

        var request = contextAccessor.HttpContext?.Request;
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var builder = request.CreateBaseUriBuilder();
        builder.PathAppend("User");
        builder.PathAppend("CreatePassword");
        builder.QuerryParameterAppend("User", Convert.ToBase64String(Encoding.UTF8.GetBytes(userDto.UserName)));
        builder.QuerryParameterAppend("Token", await userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultEmailProvider, nameof(CreateUser)));

        await emailSender.SendEmailAsync(EmailTexts.CreatedUserSubject, string.Format(EmailTexts.CreatedUserBody, builder.ToString()), userDto.Email, $"{userDto.Name} {userDto.SurName}");

        return string.Empty;
    }

    public async IAsyncEnumerable<string> CreatePassword(CreatePasswordDto dto)
    {
        var user = await userManager.FindByNameAsync(Encoding.UTF8.GetString(Convert.FromBase64String(dto.UserName)));
        if (user is null)
        {
            yield return Errors.E0001;
            yield break;
        }

        var validationResult = await userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultEmailProvider, nameof(CreateUser), dto.Token);
        if (!validationResult)
        {
            yield return Errors.E0001;
            yield break;
        }

        var changeResult = await userManager.AddPasswordAsync(user, dto.NewPassword);
        if (!changeResult.Succeeded)
        {
            foreach (var error in changeResult.Errors)
                yield return error.Description;
        }
    }

    public async IAsyncEnumerable<string> ChangePassword(string oldPassword, string newPassword)
    {
        string name = contextAccessor.HttpContext?.User.Identity?.Name ?? string.Empty;
        if (string.IsNullOrEmpty(name))
        {
            yield return Errors.E0002;
            yield break;
        }

        var user = await userManager.FindByNameAsync(name);
        if (user is null)
        {
            yield return Errors.E0002;
            yield break;
        }

        var changeResult = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (!changeResult.Succeeded)
        {
            foreach (var error in changeResult.Errors)
                yield return error.Description;
        }
    }

    public async Task<string> SignIn(string login, string password)
    {
        var user = await userManager.FindByNameAsync(login);
        if (user is null)
            return Errors.E0003;

        var result = await userManager.CheckPasswordAsync(user, password);
        if (!result)
            return Errors.E0003;

        await signInManager.SignInAsync(user, false);



        return string.Empty;
    }
}