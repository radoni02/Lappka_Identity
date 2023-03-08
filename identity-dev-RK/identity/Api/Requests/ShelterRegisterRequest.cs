namespace Api.Requests
{
    public record ShelterRegisterRequest(string OrganizationName, double Longitude, double Latitude,
        string NIP, string KRS, string PhoneNumber, UserRegistrationRequest User);
}
