using HtmlToPdf.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlToPdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentGeneratorController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentGeneratorController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var pdfFile = _documentService.GeneratePdfFromString();
            return File(pdfFile, "application/octet-stream", "SimplePdf.pdf");
        }

        [HttpGet("GetInvoicePdfFromRazor")]
        public IActionResult GetInvoicePdfFromRazor()
        {
            var pdfFile = _documentService.GeneratePdfFromRazorView("Invoice");
            return File(pdfFile, "application/octet-stream", "Invoice.pdf");
        }
        [HttpGet("GetLedgerPdfFromRazor")]
        public IActionResult GetLedgerPdfFromRazor()
        {
            var pdfFile = _documentService.GeneratePdfFromRazorView("Ledger");
            return File(pdfFile, "application/octet-stream", "Ledger.pdf");
        }
    }
}
