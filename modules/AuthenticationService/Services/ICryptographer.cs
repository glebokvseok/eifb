namespace AuthenticationService.Services;

public interface ICryptographer
{
    string Encrypt(string stringToEncrypt);
}