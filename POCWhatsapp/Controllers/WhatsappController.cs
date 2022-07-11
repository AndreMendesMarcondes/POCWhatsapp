using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using POCWhatsapp.DTO;
using System.Net.Http.Headers;
using System.Text;

namespace POCWhatsapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsappController : ControllerBase
    {
        private readonly HttpClient _client;
        public WhatsappController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://graph.facebook.com");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Environment.GetEnvironmentVariable("Token")}");
        }

        [HttpPost]
        public async Task<IActionResult> Send(WhatsappRequestDTO whatsappRequestDTO)
        {
            var whatsappRequest = new WhatsappRequest(whatsappRequestDTO);
            string jsonRequest = JsonConvert.SerializeObject(whatsappRequest);
            StringContent body = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var responseMessage =  await _client.PostAsync($"v13.0/{Environment.GetEnvironmentVariable("AppCode")}/messages", body);

            if (responseMessage.IsSuccessStatusCode)
                return StatusCode(201);
            else
            {
                return StatusCode((int)responseMessage.StatusCode, await responseMessage.Content.ReadAsStringAsync());
            }
        }
    }
}
