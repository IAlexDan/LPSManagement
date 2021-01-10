using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using LPSManagement.Server.Models;

namespace LPSManagement.Client.Pages.SendMail
{
    public class SendEmailClient
    {
        private HttpClient _httpClient;

        public SendEmailClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task SendEmail(MailRequest request)
        {
            await _httpClient.PostAsJsonAsync("api/mail/send", request);
        }
    }
}
