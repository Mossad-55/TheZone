using TheZone.Application.Common.Results;

namespace TheZone.Application.Features.Users.Errors;

public static class UserErrors
{
    public static readonly Error PhoneNumberAlreadyExists =
        new(
            "Users.PhoneNumberAlreadyExists",
            "Phone number already exists.");
}