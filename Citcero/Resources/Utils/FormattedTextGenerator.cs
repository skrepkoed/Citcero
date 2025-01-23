using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citcero.Resources.Utils
{
    class FormattedTextGenerator
    {
        public static FormattedString GetFormattedText(string text)
        {
            var FormattedText = new FormattedString();
            FormattedText.Spans.Add(new Span { Text = text });
            return FormattedText;
        }
    }
}
