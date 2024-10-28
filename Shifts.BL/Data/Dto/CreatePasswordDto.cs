namespace Shifts.BL.Data.Dto;
public class CreatePasswordDto
{
    public required string UserName { get; set; }

    public required string Token { get; set; }

    public required string NewPassword { get; set; }
}
