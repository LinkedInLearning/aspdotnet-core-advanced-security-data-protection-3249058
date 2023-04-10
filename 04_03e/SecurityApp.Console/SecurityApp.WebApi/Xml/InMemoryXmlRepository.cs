using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace SecurityApp.WebApi.Xml
{
    public class InMemoryXmlRepository : IXmlRepository
    {
        private readonly ICollection<XElement> _elements = new List<XElement>();

        public IReadOnlyCollection<XElement> GetAllElements()
        {
            return new ReadOnlyCollection<XElement>((IList<XElement>)_elements);
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _elements.Add(element);
        }
    }
}
