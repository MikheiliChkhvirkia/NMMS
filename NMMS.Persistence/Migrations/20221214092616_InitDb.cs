using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NMMS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DistributorLevelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorLevelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false, comment: "A name of the file."),
                    FileNameSufixNumber = table.Column<int>(type: "int", maxLength: 50, nullable: true, comment: "A sufix of the file."),
                    Extension = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false, comment: "An extension of the file."),
                    PhysicalPath = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "A physical path of the file."),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "A MIME type of the file."),
                    LengthInBytes = table.Column<long>(type: "bigint", nullable: false, comment: "A size of the file in bytes."),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "A date and time the file was created."),
                    DateDeleted = table.Column<DateTime>(type: "datetime", nullable: true, comment: "A date and time the file was deleted.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SexTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SexTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTypeId = table.Column<int>(type: "int", nullable: false),
                    AddressInfo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_AddressType_AddressTypeId",
                        column: x => x.AddressTypeId,
                        principalTable: "AddressType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactTypeId = table.Column<int>(type: "int", nullable: false),
                    ContactInformation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_ContactTypes_ContactTypeId",
                        column: x => x.ContactTypeId,
                        principalTable: "ContactTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "IdentificationInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentSeries = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "A date and time the file was created."),
                    DocumentTerms = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "A date and time the file was created."),
                    IdentityNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuingCompany = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentificationInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentificationInformations_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Distributors FirstName"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Distributors LastName"),
                    BirthDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())", comment: "A date and time the file was created."),
                    SexTypeId = table.Column<int>(type: "int", nullable: false),
                    RecommendedDistributorCount = table.Column<int>(type: "int", nullable: false),
                    RecommendedDistributorOverAllCount = table.Column<int>(type: "int", nullable: false),
                    RecommendatorDistributorId = table.Column<int>(type: "int", nullable: false),
                    DistributorLevelId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IdentificationInformationId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: false),
                    AddressId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Distributors_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Distributors_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Distributors_DistributorLevelTypes_SexTypeId",
                        column: x => x.SexTypeId,
                        principalTable: "DistributorLevelTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Distributors_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Distributors_IdentificationInformations_IdentificationInformationId",
                        column: x => x.IdentificationInformationId,
                        principalTable: "IdentificationInformations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Distributors_SexTypes_SexTypeId",
                        column: x => x.SexTypeId,
                        principalTable: "SexTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorId = table.Column<int>(type: "int", nullable: false),
                    SellDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    OverAllPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSales_Distributors_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Distributors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductSales_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AddressType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ფაქტიური მისამართი" },
                    { 2, "რეგისტრაციის მისამართი" }
                });

            migrationBuilder.InsertData(
                table: "ContactTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ტელეფონი" },
                    { 2, "მობილური" },
                    { 3, "ელ.ფოსტა" },
                    { 4, "ფაქსი" }
                });

            migrationBuilder.InsertData(
                table: "DistributorLevelTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "დონე 1" },
                    { 2, "დონე 2" },
                    { 3, "დონე 3" },
                    { 4, "დონე 4" },
                    { 5, "დონე 5" }
                });

            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "პირადობის მოწმობა" },
                    { 2, "პასპორტი" }
                });

            migrationBuilder.InsertData(
                table: "File",
                columns: new[] { "Id", "DateCreated", "DateDeleted", "Extension", "FileNameSufixNumber", "LengthInBytes", "MimeType", "Name", "PhysicalPath" },
                values: new object[] { new Guid("39f4cef6-6592-4a2c-9608-2a50df1b54af"), new DateTime(2022, 12, 14, 13, 26, 15, 914, DateTimeKind.Local).AddTicks(4902), null, "jpg", null, 10803564L, "image/jpeg", "DefaultFile", "D:\\NMMSFiles\\39f4cef6-6592-4a2c-9608-2a50df1b54af" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Name", "UnitPrice" },
                values: new object[,]
                {
                    { 1, new Guid("1d2a4bfc-dfb7-41b0-8c1e-cd53cdff3ccd"), "კალამი", 0.10000000000000001 },
                    { 2, new Guid("906b1a64-cdd9-4a9d-9174-5a2cd973ac2d"), "ფანქარი", 0.10000000000000001 },
                    { 3, new Guid("ea28776f-b3a7-490a-8674-469a8b19a477"), "ქაღალდი", 10.0 },
                    { 4, new Guid("39812bbd-c5d4-4ec2-8ba6-0272d922ee2c"), "მაკრატელი", 5.0 },
                    { 5, new Guid("b43f18a4-47a8-475e-93de-5b7428c08c8b"), "ფლომასტერი", 2.5 }
                });

            migrationBuilder.InsertData(
                table: "SexTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "მამრობითი" },
                    { 2, "მდედრობითი" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AddressTypeId",
                table: "Addresses",
                column: "AddressTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ContactTypeId",
                table: "Contacts",
                column: "ContactTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_AddressId",
                table: "Distributors",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_ContactId",
                table: "Distributors",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_FileId",
                table: "Distributors",
                column: "FileId",
                unique: true,
                filter: "[FileId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_IdentificationInformationId",
                table: "Distributors",
                column: "IdentificationInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Distributors_SexTypeId",
                table: "Distributors",
                column: "SexTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentificationInformations_DocumentTypeId",
                table: "IdentificationInformations",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSales_ProductsId",
                table: "ProductSales",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSales");

            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "DistributorLevelTypes");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "IdentificationInformations");

            migrationBuilder.DropTable(
                name: "SexTypes");

            migrationBuilder.DropTable(
                name: "AddressType");

            migrationBuilder.DropTable(
                name: "ContactTypes");

            migrationBuilder.DropTable(
                name: "DocumentTypes");
        }
    }
}
