using System;
using GameStore.BLL.DTO;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        bool AddNew(PublisherDTO publisherDTO);

        PublisherDTO GetByName(string companyName);

        IEnumerable<PublisherDTO> GetAll();

        bool Update(PublisherDTO publisherDTO);

        void Delete(Guid id);
    }
}
