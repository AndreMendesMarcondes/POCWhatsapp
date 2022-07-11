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
        public async Task<IActionResult> Send()
        {
            var whatsappRequest = FillDTO();
            string jsonRequest = JsonConvert.SerializeObject(whatsappRequest);
            StringContent body = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            var responseMessage = await _client.PostAsync($"v13.0/{Environment.GetEnvironmentVariable("AppCode")}/messages", body);

            if (responseMessage.IsSuccessStatusCode)
                return StatusCode(201);
            else
            {
                return StatusCode((int)responseMessage.StatusCode, await responseMessage.Content.ReadAsStringAsync());
            }
        }

        private static WhatsappRequest FillDTO()
        {
            var whatsappRequest = new WhatsappRequest();
            whatsappRequest.to = "5516991538237";
            whatsappRequest.template.name = "kabum_v0";
            whatsappRequest.template.components = new List<Component>();

            Component component = new Component();
            component.type = "body";
            component.parameters = new List<Parameter>();

            Parameter parameter1 = new Parameter();
            parameter1.type = "text";
            parameter1.text = "André";
            component.parameters.Add(parameter1);

            Parameter parameter2 = new Parameter();
            parameter2.type = "text";
            parameter2.text = "R$99,91";
            component.parameters.Add(parameter2);

            Parameter parameter3 = new Parameter();
            parameter3.type = "text";
            parameter3.text = "Kabum";
            component.parameters.Add(parameter3);

            Parameter parameter4 = new Parameter();
            parameter4.type = "text";
            parameter4.text = "André";
            component.parameters.Add(parameter4);

            whatsappRequest.template.components.Add(component);

            return whatsappRequest;
        }
    }
}
