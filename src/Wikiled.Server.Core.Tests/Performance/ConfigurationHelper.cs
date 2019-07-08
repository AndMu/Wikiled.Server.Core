using Microsoft.Extensions.Configuration;
using Moq;

namespace Wikiled.Server.Core.Tests.Performance
{
    public class ConfigurationHelper
    {
        public ConfigurationHelper()
        {
            Config = new Mock<IConfiguration>();
        }

        public Mock<IConfigurationSection> SetupSection(string name)
        {
            var section = new Mock<IConfigurationSection>();
            Config.Setup(item => item.GetSection("performance")).Returns(section.Object);
            Section = section;
            return section;
        }

        public void SetupValue(string name, string value)
        {
            var sectionValue = new Mock<IConfigurationSection>();
            sectionValue.Setup(item => item.Value).Returns(value);
            Section.Setup(item => item.GetSection(name)).Returns(sectionValue.Object);
        }

        public Mock<IConfiguration> Config { get; }

        public Mock<IConfigurationSection> Section { get; private set; }
    }
}
