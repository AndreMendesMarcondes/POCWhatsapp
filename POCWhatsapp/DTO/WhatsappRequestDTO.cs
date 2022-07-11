using Newtonsoft.Json;

namespace POCWhatsapp.DTO
{
    public class WhatsappRequestDTO
    {
        public WhatsappRequestDTO()
        {
            TemplateLanguage = "pt_BR";
        }
        public string PhoneNumber { get; set; }
        public string TemplateName { get; set; }
        public string TemplateLanguage { get; set; }
    }

    public class WhatsappRequest
    {
        public WhatsappRequest(WhatsappRequestDTO whatsappRequestDTO)
        {
            to = whatsappRequestDTO.PhoneNumber;
            template = new Template(whatsappRequestDTO.TemplateName, whatsappRequestDTO.TemplateLanguage);
        }
        public string messaging_product { get { return "whatsapp"; } }
        public string to { get; private set; }
        public string type { get { return "template"; } }
        public Template template { get; private set; }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class Template
    {
        public Template(string name, string language)
        {
            this.language = new Language(language);
            this.name = name;
        }
        public string name { get; set; }
        public Language language { get; set; }
    }

    public class Language
    {
        public Language(string code)
        {
            this.code = code;
        }
        public string code { get; set; }
    }
}
