namespace Api.Requests
{
    public record SetNewPasswordRequest(string Password,string ConfirmPassword,string Email);
}
