using Microsoft.AspNetCore.Components;

namespace Bytech.Web.Content;

/// <summary>One area of expertise. The title is always English, by design.</summary>
public sealed record Expertise(string Id, string Title, string Text, string Keywords);

/// <summary>A period of work, described by role only. Employers are left out on purpose.</summary>
public sealed record Experience(string Role, string Period, string Text);

/// <summary>
/// A client or partner, shown as a logo. Width and height are the logo's own proportions.
/// Logos are drawn as a single dark tone; set <paramref name="KeepTones"/> for marks that
/// would turn into a solid block that way.
/// </summary>
public sealed record Client(string Name, string File, int Width, int Height, bool KeepTones = false);

/// <summary>
/// All copy on the site, in one place per language. Swedish is served on "/",
/// English on "/en".
/// </summary>
public sealed record SiteText
{
    public const string LinkedInUrl = "https://www.linkedin.com/in/bennytruong/";

    public required string Lang { get; init; }
    public required string BasePath { get; init; }
    public required string OtherLangPath { get; init; }
    public required string SwitchLanguageLabel { get; init; }

    public required string PageTitle { get; init; }
    public required string MetaDescription { get; init; }

    public required string NavExpertise { get; init; }
    public required string NavAbout { get; init; }
    public required string NavExperience { get; init; }
    public required string NavContact { get; init; }
    public required string MenuLabel { get; init; }

    public required string HeroEyebrow { get; init; }
    public required string HeroTitle { get; init; }
    public required string HeroTitleAccent { get; init; }
    public required string HeroLead { get; init; }
    public required string HeroCta { get; init; }

    public required string ExpertiseHeading { get; init; }
    public required string ExpertiseLead { get; init; }
    public required IReadOnlyList<Expertise> Areas { get; init; }

    public required string AboutHeading { get; init; }
    public required string AboutStatement { get; init; }
    public required IReadOnlyList<string> AboutParagraphs { get; init; }

    public required string ExperienceHeading { get; init; }
    public required string ExperienceLead { get; init; }
    public required IReadOnlyList<Experience> Roles { get; init; }

    public required string ClientsHeading { get; init; }

    public required string ContactHeading { get; init; }
    public required string ContactLead { get; init; }
    public required string ContactCta { get; init; }

    public required string FooterBlurb { get; init; }
    public required string FooterLocation { get; init; }

    public required string NotFoundHeading { get; init; }
    public required string NotFoundText { get; init; }
    public required string NotFoundCta { get; init; }

    public bool IsEnglish => Lang == "en";

    /// <summary>Image shown when the page is shared in social media and chat apps (1200 × 630).</summary>
    public string ShareImagePath => IsEnglish ? "/img/share-en.png" : "/img/share-sv.png";

    /// <summary>Clients and partners, the same in both languages. Files live in wwwroot/img/clients.</summary>
    public static IReadOnlyList<Client> Clients { get; } =
    [
        new("Toptracer", "toptracer.png", 420, 53),
        new("Topgolf", "topgolf.svg", 360, 52),
        new("Mycronic", "mycronic.svg", 223, 32),
        new("X Shore", "x-shore.png", 640, 128),
        new("Kvadrat", "kvadrat.svg", 214, 40),
        new("Alten", "alten.svg", 74, 120, KeepTones: true),
        new("Scania", "scania.svg", 567, 94),
        new("Traton", "traton.svg", 648, 110),
        new("Assa Abloy", "assa-abloy.svg", 189, 28),
        new("Volvo", "volvo.svg", 800, 110),
    ];

    /// <summary>Link to a section on the start page. Absolute, because of &lt;base href="/"&gt;.</summary>
    public string Anchor(string id) => $"{BasePath}#{id}";

    public static SiteText For(NavigationManager nav)
    {
        var path = nav.ToBaseRelativePath(nav.Uri).Split('?', '#')[0].Trim('/');
        var english = path.Equals("en", StringComparison.OrdinalIgnoreCase)
            || path.StartsWith("en/", StringComparison.OrdinalIgnoreCase);
        return english ? En : Sv;
    }

    public static SiteText Sv { get; } = new()
    {
        Lang = "sv",
        BasePath = "/",
        OtherLangPath = "/en",
        SwitchLanguageLabel = "Switch to English",

        PageTitle = "Bytech | Benny Truong – Teknikkonsult inom IT och mjukvara",
        MetaDescription = "Bytech är Benny Truongs konsultbolag i Stockholm. Teknikkonsult inom tekniskt ledarskap, mjukvaruutveckling, systemarkitektur, IT-infrastruktur, DevOps och R&D.",

        NavExpertise = "Kompetens",
        NavAbout = "Om",
        NavExperience = "Erfarenhet",
        NavContact = "Kontakt",
        MenuLabel = "Meny",

        HeroEyebrow = "Benny Truong · Teknikkonsult inom IT och mjukvara",
        HeroTitle = "Tekniskt ledarskap,",
        HeroTitleAccent = "från idé till drift.",
        HeroLead = "Bytech hjälper organisationer att bygga mjukvara, team och plattformar som håller över tid.",
        HeroCta = "Ta kontakt",

        ExpertiseHeading = "Kompetens",
        ExpertiseLead = "Fyra områden där jag gör störst skillnad.",
        Areas =
        [
            new("tech-lead", "Tech Lead / Engineering Management",
                "Jag har lett agila, tvärfunktionella team som R&D-chef, konsultchef och teknisk ledare – och knyter ihop vision, roadmap och backlog till leveranser som håller.",
                "Ledarskap · Produktägarskap · Scrum & SAFe"),
            new("software", "Software Development",
                "Från realtidsmjukvara i C för elektrifierade fordon till fullstack-webb i Python. Arkitektur, krav och kod som går att testa och förvalta.",
                "Systemarkitektur · Python & C · Inbyggda system"),
            new("infrastructure", "IT Infrastructure",
                "Molnbaserade plattformar och utvecklarmiljöer byggda som kod – med tydlig styrning och säkerhet i linje med NIS2 och CRA.",
                "Kubernetes & Azure · Infrastructure as Code · CI/CD"),
            new("data-ai", "Data & AI",
                "Gemensamma data- och analysplattformar med tydligt ägarskap och styrning – grunden som krävs för att data och AI ska ge verklig nytta.",
                "Dataplattformar · Analys · Datastyrning"),
        ],

        AboutHeading = "Om Bytech",
        AboutStatement = "Bakom Bytech står Benny Truong – civilingenjör från KTH med över 13 års erfarenhet av mjukvaruutveckling och IT-infrastruktur.",
        AboutParagraphs =
        [
            "Kombinerar hands-on förmåga att kunna programmera och sätta upp servrar med systemarkitektur och förmågan att bygga och leda tekniskt tunga team med både seniora och unga utvecklare.",
            "Kombinerar tekniskt ledarskap med ett pragmatiskt ingenjörsperspektiv och har erfarenhet inom IT-infrastruktur, DevOps, IT-säkerhet samt att driva monitorering, loggning, backup och att drifta IT-tjänster och servrar.",
            "Har många års erfarenhet av att leda agila, tvärfunktionella team, driva igenom arkitekturbeslut, förbättra utvecklingsprocesser samt omsätta verksamhetsbehov till skalbara tekniska lösningar.",
            "Bytech tar konsultuppdrag inom arkitektur, utveckling och tekniskt ledarskap. Förmågan att kunna koordinera och kommunicera i en komplex organisation och skapa personliga kontaktytor skapar en effektiv balans mellan människa och teknik.",
        ],

        ExperienceHeading = "Erfarenhet",
        ExperienceLead = "Över tretton år i roller mellan kod, arkitektur och ledning.",
        Roles =
        [
            new("Senior systemarkitekt", "2026 –",
                "Driver mjukvaruförändringar från idé till överenskommen systemlösning i en fordonsplattform som delas av flera varumärken."),
            new("R&D-chef, Data & IT-infrastruktur", "2021 – 2026",
                "Byggde upp team och molnbaserade data- och utvecklarplattformar, och ledde IT-styrningen över flera divisioner."),
            new("Konsultchef & teknisk ledare", "2019 – 2021",
                "Ledde seniora mjukvarukonsulter och tog en helt eldriven båtprototyp från integration till första demo för investerare."),
            new("Mjukvaruutvecklare & arkitekt", "2013 – 2019",
                "Styrsystem och mjukvaruarkitektur för el- och hybridfordon – från arbetsmaskiner till lastbilar på elväg – och en patenterad metod för bromsstyrning."),
        ],

        ClientsHeading = "Kunder & Partners",

        ContactHeading = "Har du ett uppdrag i tankarna?",
        ContactLead = "Hör av dig, så tar vi det därifrån.",
        ContactCta = "Kontakta mig på LinkedIn",

        FooterBlurb = "Benny Truongs konsultbolag inom tekniskt ledarskap, mjukvaruutveckling och IT-infrastruktur – med bas i Stockholm.",
        FooterLocation = "Stockholm, Sverige",

        NotFoundHeading = "Sidan finns inte",
        NotFoundText = "Sidan du letar efter har flyttat eller har aldrig funnits.",
        NotFoundCta = "Till startsidan",
    };

    public static SiteText En { get; } = new()
    {
        Lang = "en",
        BasePath = "/en",
        OtherLangPath = "/",
        SwitchLanguageLabel = "Byt till svenska",

        PageTitle = "Bytech | Benny Truong – IT & Software Engineering Consultant",
        MetaDescription = "Bytech is Benny Truong's consultancy in Stockholm, Sweden: technical leadership, software development, system architecture, IT infrastructure, DevOps and R&D.",

        NavExpertise = "Expertise",
        NavAbout = "About",
        NavExperience = "Experience",
        NavContact = "Contact",
        MenuLabel = "Menu",

        HeroEyebrow = "Benny Truong · IT and software engineering consultant",
        HeroTitle = "Technical leadership,",
        HeroTitleAccent = "from idea to operations.",
        HeroLead = "Bytech helps organisations build software, teams and platforms that stand the test of time.",
        HeroCta = "Get in touch",

        ExpertiseHeading = "Expertise",
        ExpertiseLead = "Four areas where I make the biggest difference.",
        Areas =
        [
            new("tech-lead", "Tech Lead / Engineering Management",
                "I have led agile, cross-functional teams as R&D manager, consulting manager and technical lead – tying vision, roadmap and backlog into deliveries that hold up.",
                "Leadership · Product ownership · Scrum & SAFe"),
            new("software", "Software Development",
                "From real-time software in C for electrified vehicles to full-stack web in Python. Architecture, requirements and code that can be tested and maintained.",
                "System architecture · Python & C · Embedded systems"),
            new("infrastructure", "IT Infrastructure",
                "Cloud-based platforms and developer environments built as code – with clear governance and security in line with NIS2 and CRA.",
                "Kubernetes & Azure · Infrastructure as Code · CI/CD"),
            new("data-ai", "Data & AI",
                "Shared data and analytics platforms with clear ownership and governance – the foundation data and AI need to create real value.",
                "Data platforms · Analytics · Data governance"),
        ],

        AboutHeading = "About Bytech",
        AboutStatement = "Behind Bytech is Benny Truong – an M.Sc. engineer from KTH with more than 13 years of experience in software development and IT infrastructure.",
        AboutParagraphs =
        [
            "Combines the hands-on ability to write code and set up servers with system architecture and the ability to build and lead technically heavy teams of both senior and junior developers.",
            "Combines technical leadership with a pragmatic engineering perspective, with experience in IT infrastructure, DevOps and IT security, as well as running monitoring, logging, backup and the operation of IT services and servers.",
            "Has many years of experience leading agile, cross-functional teams, driving architecture decisions, improving development processes and turning business needs into scalable technical solutions.",
            "Bytech takes on consulting assignments in architecture, development and technical leadership. The ability to coordinate and communicate in a complex organisation and to build personal relationships creates an effective balance between people and technology.",
        ],

        ExperienceHeading = "Experience",
        ExperienceLead = "More than thirteen years in roles spanning code, architecture and management.",
        Roles =
        [
            new("Senior Systems Architect", "2026 –",
                "Driving software changes from idea to agreed system solution in a vehicle platform shared by several brands."),
            new("R&D Manager, Data & IT Infrastructure", "2021 – 2026",
                "Built up teams and cloud-based data and developer platforms, and led IT governance across several divisions."),
            new("Consulting Manager & Technical Lead", "2019 – 2021",
                "Led senior software consultants and took a fully electric boat prototype from integration to its first investor demo."),
            new("Software Developer & Architect", "2013 – 2019",
                "Control systems and software architecture for electric and hybrid vehicles – from construction machines to trucks on electric roads – and a patented brake control method."),
        ],

        ClientsHeading = "Clients & Partners",

        ContactHeading = "Have an assignment in mind?",
        ContactLead = "Get in touch and we'll take it from there.",
        ContactCta = "Contact me on LinkedIn",

        FooterBlurb = "Benny Truong's consultancy in technical leadership, software development and IT infrastructure – based in Stockholm.",
        FooterLocation = "Stockholm, Sweden",

        NotFoundHeading = "Page not found",
        NotFoundText = "The page you are looking for has moved or never existed.",
        NotFoundCta = "Back to start",
    };
}
