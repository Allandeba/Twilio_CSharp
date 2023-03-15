// Install the C# / .NET helper library from twilio.com/docs/csharp/install

using System;
using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

class Program
{
    static void Main(string[] args)
    {
        SendCall().Wait();
        Console.Write("Press any key to continue.");
        Console.ReadKey();
    }

    static async Task SendCall()
    {
        try
        {
            String AccountSid = Environment.GetEnvironmentVariable("TWILIO_ACCOUNT_SID") ?? "";
            String AuthToken = Environment.GetEnvironmentVariable("TWILIO_AUTH_TOKEN") ?? "";
            String FromNumber = "+15403860076";
            String ToNumber = Environment.GetEnvironmentVariable("MY_PHONE_NUMBER") ?? "";

            // Create Twilio client with credentials
            TwilioClient.Init(AccountSid, AuthToken);

            // Make call using Twilio API
            var call = await CallResource.CreateAsync(
                to: new Twilio.Types.PhoneNumber(ToNumber),
                from: new Twilio.Types.PhoneNumber(FromNumber),
                url: new Uri("http://demo.twilio.com/docs/voice.xml")
            );   

            Console.WriteLine(call.Sid);
        }
        catch (ApiException exception)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine($"Twilio Error {exception.Code} - {exception.MoreInfo}");
        }
    }    
}
