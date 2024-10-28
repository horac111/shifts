namespace Shifts.WebServices.Data;

public class SmtpSettings
{
    public required string Url { get; init; }

    public required int Port { get; init; }

    public required bool UseSsl { get; init; }

    public required string Login { get; init; }

    public required string Password { get; init; }

    public required string Email { get; init; }

    public required string Name { get; init; }


}
