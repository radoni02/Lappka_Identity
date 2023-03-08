namespace Api.Requests
{
    public record UserRegistrationRequest(string FirstName,string LastName,string EmailAddress,string Password,string ConfirmPassword);
    
}
