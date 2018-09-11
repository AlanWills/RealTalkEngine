using System;

using Alexa.NET.Management;

namespace RealTalkSkillManagement
{
    public class Class1
    {
        public Class1()
        {
            var settings = await Settings.GetClientDetails();
            var amazonLogin = new AmazonLogin(settings.ClientId, settings.ClientSecret);
            var api = new ManagementApi(await amazonLogin.TokenAuthorizer());
            var vendorResponse = await api.Vendors.Get();
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }
    }
}
