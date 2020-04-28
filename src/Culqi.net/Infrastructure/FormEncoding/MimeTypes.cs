using System;
using System.Collections.Generic;
using System.Text;

namespace Culqi.Infrastructure.FormEncoding
{
    internal static class MimeTypes
    {
        private static readonly IDictionary<string, string> MimeTypeMap
            = new Dictionary<string, string>
        {
            { ".csv", "text/csv" },
            { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
            { ".gif", "image/gif" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".pdf", "application/pdf" },
            { ".png", "image/png" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        };

        public static string GetMimeType(string extension)
        {
            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            if (!extension.StartsWith("."))
            {
                extension = "." + extension;
            }

            string mime;

            return MimeTypeMap.TryGetValue(extension, out mime) ? mime : "application/octet-stream";
        }
    }
}
