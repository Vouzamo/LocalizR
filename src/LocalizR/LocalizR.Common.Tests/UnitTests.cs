using LocalizR.Common.Models;
using LocalizR.Common.Services;
using LocalizR.Common.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LocalizR.Common.Tests
{
    [TestClass]
    public class UnitTests
    {
        private static readonly Example Example = new()
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

        [TestMethod]
        public void CurrentCultureTests()
        {
            ILocalizationService service = new CurrentCultureLocalizationService();

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");

            Assert.AreEqual("Hello world!", service.Localize(Example.Title));
            Assert.AreEqual(2, service.Localize(Example.Tags)?.Count());
            Assert.IsFalse(service.Localize(Example.DisplayAuthor));

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr");

            Assert.AreEqual("Bonjour le monde!", service.Localize(Example.Title));
            Assert.IsNull(service.Localize(Example.Tags));
            Assert.IsTrue(service.Localize(Example.DisplayAuthor));

            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("de");

            Assert.AreEqual("Hallo welt!", service.Localize(Example.Title));
            Assert.AreEqual(1, service.Localize(Example.Tags)?.Count());
            Assert.IsFalse(service.Localize(Example.DisplayAuthor));
        }

        [TestMethod]
        public void HierarchyTests()
        {
            var localizationHierarchy = new Hierarchy<string>("en")
            {
                new Hierarchy<string>("fr")
                {
                    new Hierarchy<string>("fr-FR")
                },
                new Hierarchy<string>("es")
                {
                    new Hierarchy<string>("es-ES")
                },
                new Hierarchy<string>("de")
                {
                    new Hierarchy<string>("de-DE")
                },
                new Hierarchy<string>("en-GB"),
                new Hierarchy<string>("en-US")
            };

            if(localizationHierarchy.TryFindDependencyChain(l => l.Equals("fr-FR"), out var chain))
            {
                ILocalizationService service = new ChainLocalizationService(chain);

                Assert.AreEqual("Bonjour le monde!", service.Localize(Example.Title));
                Assert.AreEqual(2, service.Localize(Example.Tags)?.Count());
                Assert.IsTrue(service.Localize(Example.DisplayAuthor));
            }
        }
    }
}