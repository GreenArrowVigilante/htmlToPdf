using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlToPdf.Interfaces
{
    public interface IDocumentService
    {
        byte[] GeneratePdfFromString();

        byte[] GeneratePdfFromRazorView(string FilePath);
    }
}
