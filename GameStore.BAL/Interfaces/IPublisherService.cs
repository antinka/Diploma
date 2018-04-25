using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        void AddNew(PublisherDTO publisherDTO);

        PublisherDTO GetByName(string companyName);

        IEnumerable<PublisherDTO> GetAll();
    }
}
