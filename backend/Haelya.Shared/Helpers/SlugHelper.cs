using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Haelya.Shared.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string phrase)
        {
            string normalized = phrase.ToLowerInvariant();
            normalized = RemoveDiacritics(normalized);
            normalized = Regex.Replace(normalized, @"[^a-z0-9\s-]", ""); // remove invalid chars
            normalized = Regex.Replace(normalized, @"\s+", "-").Trim('-'); // convert spaces to dashes
            normalized = Regex.Replace(normalized, @"-+", "-"); // remove duplicate dashes
            return normalized;
        }

        private static string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
