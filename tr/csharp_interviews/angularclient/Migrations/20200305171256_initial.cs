using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace angularclient.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    KeywordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    IsRequired = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.KeywordId);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ListUrl = table.Column<string>(nullable: true),
                    UrlPath = table.Column<string>(nullable: true),
                    DatePath = table.Column<string>(nullable: true),
                    PublisherPath = table.Column<string>(nullable: true),
                    TitlePath = table.Column<string>(nullable: true),
                    DescriptionPath = table.Column<string>(nullable: true),
                    EmptyPageIndicatorPath = table.Column<string>(nullable: true),
                    DateExtractor = table.Column<int>(nullable: true),
                    UrlTransformer = table.Column<int>(nullable: true),
                    IsJobIdInQueryParam = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.ProviderId);
                });

            migrationBuilder.CreateTable(
                name: "SearchJobs",
                columns: table => new
                {
                    SearchJobId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SearchDate = table.Column<DateTime>(nullable: false),
                    DurationInMs = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchJobs", x => x.SearchJobId);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionUrlTransformers",
                columns: table => new
                {
                    DescriptionUrlTransformerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: true),
                    Replacer = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionUrlTransformers", x => x.DescriptionUrlTransformerId);
                    table.ForeignKey(
                        name: "FK_DescriptionUrlTransformers_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrlSpecialCharacters",
                columns: table => new
                {
                    UrlSpecialCharacterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(nullable: false),
                    Replacer = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlSpecialCharacters", x => x.UrlSpecialCharacterId);
                    table.ForeignKey(
                        name: "FK_UrlSpecialCharacters_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CachedUrls",
                columns: table => new
                {
                    CachedUrlId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: false),
                    SearchJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CachedUrls", x => x.CachedUrlId);
                    table.ForeignKey(
                        name: "FK_CachedUrls_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CachedUrls_SearchJobs_SearchJobId",
                        column: x => x.SearchJobId,
                        principalTable: "SearchJobs",
                        principalColumn: "SearchJobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultEntities",
                columns: table => new
                {
                    ResultEntityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Publisher = table.Column<string>(nullable: true),
                    RefUrl = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: false),
                    SearchJobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultEntities", x => x.ResultEntityId);
                    table.ForeignKey(
                        name: "FK_ResultEntities_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "ProviderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultEntities_SearchJobs_SearchJobId",
                        column: x => x.SearchJobId,
                        principalTable: "SearchJobs",
                        principalColumn: "SearchJobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Keywords",
                columns: new[] { "KeywordId", "IsRequired", "Value" },
                values: new object[,]
                {
                    { 1, true, "wcf" },
                    { 24, true, "asmx" },
                    { 23, true, "winform" },
                    { 22, true, "webform" },
                    { 21, false, "docker" },
                    { 20, false, "aws" },
                    { 19, false, "windows communication foundation" },
                    { 18, false, "sql server" },
                    { 17, false, "visual studio" },
                    { 16, false, "identity" },
                    { 15, false, "mssql" },
                    { 14, false, "oauth" },
                    { 13, false, "openid" },
                    { 12, false, "senior" },
                    { 11, false, "english" },
                    { 10, false, "anglais" },
                    { 9, false, "angular" },
                    { 8, false, "core" },
                    { 7, false, "jquery" },
                    { 6, true, "asp.net" },
                    { 5, true, "dotnet" },
                    { 4, true, "csharp" },
                    { 3, true, "c#" },
                    { 2, true, "asp" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "DateExtractor", "DatePath", "DescriptionPath", "EmptyPageIndicatorPath", "IsJobIdInQueryParam", "ListUrl", "PublisherPath", "TitlePath", "UrlPath", "UrlTransformer" },
                values: new object[,]
                {
                    { 1, null, "//div[contains(@class, 'roffre')]/div/div[2]/span[2]", "//div[@id = 'divcontmain-pad']/div[2]/div[3]", "//div[contains(@class, 'roffre')]", false, "https://www.freelance-info.fr/missions.php?f=ile_de_france&mots={keyword}&tri=date&p={pageindex}", "//div[contains(@class, 'roffre')]/div/div[1]/div/img", "//div[contains(@class, 'roffre')]/div/div[2]/div[1]", "//div[contains(@class, 'roffre')]/div/div[2]/div[1]/a", null },
                    { 2, null, "//div[contains(@class, 'result-link-bar')]/div/span[contains(@class, 'date')]", "//div[contains(@class, 'jobsearch-JobComponent-description')]", "//div[contains(@class, 'jobsearch-SerpJobCard')]", true, "https://www.indeed.fr/emplois?as_and={keyword}&as_phr=&as_any=&as_not=&as_ttl=&as_cmp=&jt=subcontract&st=&as_src=&salary=&radius=25&l=%C3%8Ele-de-France&fromage=any&limit=50&sort=date&psf=advsrch&from=advancedsearch", "//div[contains(@class, 'sjcl')]/div/span[contains(@class, 'company')]", "//div[contains(@class, 'title')]", "//div[contains(@class, 'title')]/a", null }
                });

            migrationBuilder.InsertData(
                table: "DescriptionUrlTransformers",
                columns: new[] { "DescriptionUrlTransformerId", "ProviderId", "Replacer", "Value" },
                values: new object[] { 2, 2, "voir-emploi", "rc/clk" });

            migrationBuilder.InsertData(
                table: "UrlSpecialCharacters",
                columns: new[] { "UrlSpecialCharacterId", "ProviderId", "Replacer", "Value" },
                values: new object[] { 1, 1, "%23", "#" });

            migrationBuilder.InsertData(
                table: "UrlSpecialCharacters",
                columns: new[] { "UrlSpecialCharacterId", "ProviderId", "Replacer", "Value" },
                values: new object[] { 2, 2, "%23", "#" });

            migrationBuilder.CreateIndex(
                name: "IX_CachedUrls_ProviderId",
                table: "CachedUrls",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_CachedUrls_SearchJobId",
                table: "CachedUrls",
                column: "SearchJobId");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionUrlTransformers_ProviderId",
                table: "DescriptionUrlTransformers",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultEntities_ProviderId",
                table: "ResultEntities",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ResultEntities_SearchJobId",
                table: "ResultEntities",
                column: "SearchJobId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlSpecialCharacters_ProviderId",
                table: "UrlSpecialCharacters",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CachedUrls");

            migrationBuilder.DropTable(
                name: "DescriptionUrlTransformers");

            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "ResultEntities");

            migrationBuilder.DropTable(
                name: "UrlSpecialCharacters");

            migrationBuilder.DropTable(
                name: "SearchJobs");

            migrationBuilder.DropTable(
                name: "Providers");
        }
    }
}
