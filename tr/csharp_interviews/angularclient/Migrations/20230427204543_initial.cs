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
                    KeywordId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    KeywordType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.KeywordId);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    ProviderId = table.Column<int>(nullable: false),
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
                    UrlSpecialCharacterId = table.Column<int>(nullable: false),
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
                columns: new[] { "KeywordId", "KeywordType", "Value" },
                values: new object[,]
                {
                    { 1, 0, "diversit" },
                    { 2, 0, "inclusion" },
                    { 3, 0, "handicap" },
                    { 4, 0, "discrimination" },
                    { 5, 0, "égalité" },
                    { 6, 2, "cdd" },
                    { 7, 2, "cdi" },
                    { 8, 2, "intérim" },
                    { 9, 2, "provisoir" },
                    { 10, 2, "anglais" },
                    { 11, 2, "english" },
                    { 12, 2, "junior" },
                    { 13, 2, "débutant" },
                    { 14, 1, "insertion" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ProviderId", "DateExtractor", "DatePath", "DescriptionPath", "EmptyPageIndicatorPath", "IsJobIdInQueryParam", "ListUrl", "PublisherPath", "TitlePath", "UrlPath", "UrlTransformer" },
                values: new object[] { 1, null, "//div[contains(@class, 'cardOutline')]/div/div[1]/div/div/table[2]/tbody/tr[2]/td/div/span[1]/text()", "//div[contains(@id, 'jobDescriptionText')]", "//div[contains(@class, 'jobsearch-NoResult-messageHeader')]", false, "https://fr.indeed.com/jobs?q={keyword}&l=%C3%8Ele-de-France&sort=date", "//div[contains(@class, 'companyInfo')]/span[1]", "//div[contains(@class, 'cardOutline')]/div/div[1]/div/div/table/tbody/tr/td/div/h2/a", "//div[contains(@class, 'cardOutline')]/div/div[1]/div/div/table/tbody/tr/td/div/h2/a/@href", null });

            migrationBuilder.InsertData(
                table: "UrlSpecialCharacters",
                columns: new[] { "UrlSpecialCharacterId", "ProviderId", "Replacer", "Value" },
                values: new object[] { 1, 1, "%23", "#" });

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
