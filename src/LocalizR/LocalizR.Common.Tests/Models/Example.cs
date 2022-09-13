using LocalizR.Common.Models;
using System;
using System.Collections.Generic;

namespace LocalizR.Common.Tests.Models
{
    public class Example
    {
        public Localizable<string>? Title { get; set; }
        public Localizable<IEnumerable<string>>? Tags { get; set; }
        public DateTime PublishedDate { get; set; }
        public Localizable<bool>? DisplayAuthor { get; set; }
    }
}
