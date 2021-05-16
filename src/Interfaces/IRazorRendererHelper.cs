using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HtmlToPdf.Interfaces
{
    public interface IRazorRendererHelper
    {
        string RenderPartialToString<TModel>(string partialName, TModel model);
    }
}
