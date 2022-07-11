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
        public string TemplateName { get; private set; }
        public string TemplateLanguage { get; set; }
    }

    public class WhatsappRequest
    {
        public WhatsappRequest()
        {
            template = new Template();
        }
        public string messaging_product { get { return "whatsapp"; } }
        public string to { get; set; }
        public string type { get { return "template"; } }
        public Template template { get; set; }

        public override string? ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class Template
    {
        public Template()
        {
            this.language = new Language();
            this.name = name;
            components = new List<Component>();
        }
        public string name { get; set; }
        public Language language { get; set; }
        public List<Component> components { get; set; }
    }

    public class Language
    {
        public string code { get { return "pt_BR"; } }
    }

    public class Component
    {
        public string type { get; set; }
        public List<Parameter> parameters { get; set; }
    }

    public class Parameter
    {
        public string type { get; set; }
        public string text { get; set; }
    }
}
