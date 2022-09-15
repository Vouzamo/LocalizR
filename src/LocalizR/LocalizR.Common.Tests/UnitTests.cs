using LocalizR.Common.Accessors;
using LocalizR.Common.Models;
using LocalizR.Common.Services;
using LocalizR.Common.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace LocalizR.Common.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void HierarchyTests()
        {
            var example = new Example
            {
                Title = new Localizable<string>()
                {
                    { "en", "Hello world!" },
                    { "fr", "Bonjour le monde!" },
                    { "de", "Hallo welt!" }
                },
                    Tags = new Localizable<IEnumerable<string>>()
                {
                    { "en", new List<string> { "new", "featured" } },
                    { "de", new List<string> { "featured" } }
                },
                    DisplayAuthor = new Localizable<bool>()
                {
                    { "en", false },
                    { "fr", true }
                }
            };

            ILocalizationHierarchyAccessor<CultureInfo> localizationHierarchyAccessor = new DefaultLocalizationHierarchyAccessor<CultureInfo>(new Hierarchy<CultureInfo>(new CultureInfo("en"))
            {
                new Hierarchy<CultureInfo>(new CultureInfo("fr"))
                {
                    new Hierarchy<CultureInfo>(new CultureInfo("fr-FR"))
                },
                new Hierarchy<CultureInfo>(new CultureInfo("es"))
                {
                    new Hierarchy<CultureInfo>(new CultureInfo("es-ES"))
                },
                new Hierarchy<CultureInfo>(new CultureInfo("de"))
                {
                    new Hierarchy<CultureInfo>(new CultureInfo("de-DE"))
                },
                new Hierarchy<CultureInfo>(new CultureInfo("en-GB")),
                new Hierarchy<CultureInfo>(new CultureInfo("en-US"))
            });

            ILocalizationAccessor<CultureInfo> localizationAccessor = new CurrentThreadLocalizationAccessor();

            ILocalizationService service = new CultureInfoLocalizationService(localizationHierarchyAccessor, localizationAccessor);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");

            Assert.AreEqual("Hello world!", service.Localize(example.Title));
            Assert.AreEqual(2, service.Localize(example.Tags)?.Count());
            Assert.IsFalse(service.Localize(example.DisplayAuthor));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr");

            Assert.AreEqual("Bonjour le monde!", service.Localize(example.Title));
            Assert.AreEqual(2, service.Localize(example.Tags)?.Count());
            Assert.IsTrue(service.Localize(example.DisplayAuthor));

            Thread.CurrentThread.CurrentCulture = new CultureInfo("de");

            Assert.AreEqual("Hallo welt!", service.Localize(example.Title));
            Assert.AreEqual(1, service.Localize(example.Tags)?.Count());
            Assert.IsFalse(service.Localize(example.DisplayAuthor));
        }
    }
}