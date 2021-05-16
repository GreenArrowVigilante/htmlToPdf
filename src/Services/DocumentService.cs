using DinkToPdf;
using DinkToPdf.Contracts;
using HtmlToPdf.Interfaces;
using HtmlToPdf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlToPdf.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IConverter _converter;
        private readonly IRazorRendererHelper _razorRendererHelper;


        public DocumentService(
            IConverter converter,
            IRazorRendererHelper razorRendererHelper)
        {
            _converter = converter;
            _razorRendererHelper = razorRendererHelper;
        }

        public byte[] GeneratePdfFromString()
        {
            var htmlContent = $@"
            <!DOCTYPE html>
            <html lang=""en"">
            <head>
                <style>
                p{{
                    width: 80%;
                }}
                </style>
            </head>
            <body>
                <h1>Some heading</h1>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            </body>
            </html>
            ";

            return GeneratePdf(htmlContent);
        }

        public byte[] GeneratePdfFromRazorView(string FileName)
        {
            var (model, filePath) = GetFilePath(FileName);
            var viewModel = model;
            var partialName = filePath;
            var htmlContent = _razorRendererHelper.RenderPartialToString(partialName, viewModel);

            return GeneratePdf(htmlContent);
        }
        public (object model,string filePath) GetFilePath(string FileName) 
        {
            string filePath = "";
            object model = null;
            switch (FileName)
            {
                case "Invoice":
                    filePath = "/Views/PdfTemplate/InvoiceDetails.cshtml";
                    model = GetInvoiceModel();
                    break;
                case "Ledger":
                    filePath = "/Views/PdfTemplate/LedgerDetails.cshtml";
                    model = GetLedgerModel();
                    break;
                default:

                    break;
            }
            return (model, filePath);
        }

        private byte[] GeneratePdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10 },
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontSize = 10, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontSize = 8, Center = "GreenArrowVigilante", Line = true },
            };

            var htmlToPdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings },
            };

            return _converter.Convert(htmlToPdfDocument);
        }

        private InvoiceViewModel GetInvoiceModel()
        {
            var invoiceViewModel = new InvoiceViewModel
            {
                OrderDate = DateTime.Now,
                OrderId = 1234567890,
                DeliveryDate = DateTime.Now.AddDays(10),
                Products = new List<Product>()
                {
                    new Product
                    {
                        ItemName = "Hosting (12 months)",
                        Price = 200
                    },
                    new Product
                    {
                        ItemName = "Domain name (1 year)",
                        Price = 12
                    },
                    new Product
                    {
                        ItemName = "Website design",
                        Price = 1000

                    },
                    new Product
                    {
                        ItemName = "Maintenance",
                        Price = 300
                    },
                    new Product
                    {
                        ItemName = "Customization",
                        Price = 400
                    },
                }
            };

            invoiceViewModel.TotalAmount = invoiceViewModel.Products.Sum(p => p.Price);

            return invoiceViewModel;
        }

        private LedgerViewModel GetLedgerModel()
        {
            var ledgerViewModel = new LedgerViewModel
            {
                PartyName = "John Smith",
                AsOn = DateTime.Now,
                FinancialPeriod = "2020-21",
                LedgerDetails = new List<Ledger>()
                {
                    new Ledger
                    {
                        Particular = "Opening Balance",
                        Debit = 2000,
                        Credit = 0,
                        Balance = 2000
                    },
                    new Ledger
                    {
                        Particular = "Reciept",
                        Debit = 1000,
                        Credit = 0,
                        Balance = 3000
                    },
                    new Ledger
                    {
                        Particular = "Reciept",
                        Debit = 1500,
                        Credit = 0,
                        Balance = 4500

                    },
                    new Ledger
                    {
                        Particular = "Payment",
                        Debit = 0,
                        Credit = 2250,
                        Balance = 2250
                    },
                    new Ledger
                    {   
                        Particular = "Recipt",
                        Debit =1000,
                        Credit = 0,
                        Balance = 3250
                    },
                }
            };

            ledgerViewModel.ClosingBalance = ledgerViewModel.LedgerDetails.LastOrDefault().Balance;

            return ledgerViewModel;
        }
    }
}
