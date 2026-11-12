using System.Text.Json;

namespace Bytech.Web.Content;

/// <summary>
/// schema.org data (JSON-LD) for the start page. It tells search engines and AI agents, in a
/// form they can read without guessing, who is behind Bytech and what the company does.
/// Employers are left out here too.
/// </summary>
public static class StructuredData
{
    public const string SiteUrl = "https://bytech.se";

    private const string WebsiteId = SiteUrl + "/#website";
    private const string CompanyId = SiteUrl + "/#bytech";
    private const string PersonId = SiteUrl + "/#benny-truong";

    private static readonly string[] SkillsSv =
    [
        "Tekniskt ledarskap", "Engineering management", "Tech lead", "Mjukvaruutveckling",
        "Systemarkitektur", "Systemering", "IT-infrastruktur", "DevOps", "CI/CD", "Kubernetes",
        "Microsoft Azure", "Infrastructure as Code", "IT-säkerhet", "Monitorering och loggning",
        "Dataplattformar", "Data och AI", "Inbyggda system", "Python", "C", "Agila metoder",
        "Scrum", "SAFe", "Forskning och utveckling (R&D)",
    ];

    private static readonly string[] SkillsEn =
    [
        "Technical leadership", "Engineering management", "Tech lead", "Software development",
        "System architecture", "Systems design", "IT infrastructure", "DevOps", "CI/CD", "Kubernetes",
        "Microsoft Azure", "Infrastructure as Code", "IT security", "Monitoring and logging",
        "Data platforms", "Data and AI", "Embedded systems", "Python", "C", "Agile methods",
        "Scrum", "SAFe", "Research and development (R&D)",
    ];

    /// <summary>The JSON-LD document for the start page in the language of <paramref name="t"/>.</summary>
    public static string For(SiteText t)
    {
        var pageUrl = SiteUrl + t.BasePath;
        var skills = t.IsEnglish ? SkillsEn : SkillsSv;

        var website = new Dictionary<string, object>
        {
            ["@type"] = "WebSite",
            ["@id"] = WebsiteId,
            ["url"] = SiteUrl + "/",
            ["name"] = "Bytech",
            ["alternateName"] = "Bytech – Benny Truong",
            ["inLanguage"] = new[] { "sv", "en" },
            ["publisher"] = Ref(CompanyId),
        };

        var company = new Dictionary<string, object>
        {
            ["@type"] = "ProfessionalService",
            ["@id"] = CompanyId,
            ["name"] = "Bytech",
            ["url"] = SiteUrl + "/",
            ["description"] = t.MetaDescription,
            ["logo"] = SiteUrl + "/img/bytech-logo.png",
            ["image"] = SiteUrl + t.ShareImagePath,
            ["founder"] = Ref(PersonId),
            ["address"] = new Dictionary<string, object>
            {
                ["@type"] = "PostalAddress",
                ["addressLocality"] = "Stockholm",
                ["addressCountry"] = "SE",
            },
            ["areaServed"] = new Dictionary<string, object>
            {
                ["@type"] = "Country",
                ["name"] = t.IsEnglish ? "Sweden" : "Sverige",
            },
            ["knowsAbout"] = t.Areas.Select(area => area.Title).ToArray(),
        };

        var person = new Dictionary<string, object>
        {
            ["@type"] = "Person",
            ["@id"] = PersonId,
            ["name"] = "Benny Truong",
            ["givenName"] = "Benny",
            ["familyName"] = "Truong",
            ["jobTitle"] = t.IsEnglish
                ? "Technology consultant, systems architect and technical lead"
                : "Teknikkonsult, systemarkitekt och teknisk ledare",
            ["description"] = t.AboutStatement,
            ["url"] = SiteUrl + "/",
            ["sameAs"] = new[] { SiteText.LinkedInUrl },
            ["worksFor"] = Ref(CompanyId),
            ["alumniOf"] = new Dictionary<string, object>
            {
                ["@type"] = "CollegeOrUniversity",
                ["name"] = t.IsEnglish ? "KTH Royal Institute of Technology" : "KTH Kungliga Tekniska högskolan",
                ["sameAs"] = "https://www.kth.se/",
            },
            ["knowsAbout"] = skills,
            ["knowsLanguage"] = new[] { "sv", "en" },
        };

        var page = new Dictionary<string, object>
        {
            ["@type"] = "WebPage",
            ["@id"] = pageUrl + "#page",
            ["url"] = pageUrl,
            ["name"] = t.PageTitle,
            ["description"] = t.MetaDescription,
            ["inLanguage"] = t.Lang,
            ["isPartOf"] = Ref(WebsiteId),
            ["about"] = Ref(CompanyId),
            ["mainEntity"] = Ref(PersonId),
        };

        var document = new Dictionary<string, object>
        {
            ["@context"] = "https://schema.org",
            ["@graph"] = new object[] { website, company, person, page },
        };

        // The default encoder escapes <, > and & as \uXXXX, so the result is safe inside <script>.
        return JsonSerializer.Serialize(document);
    }

    private static Dictionary<string, object> Ref(string id) => new() { ["@id"] = id };
}
