using GameStore.BLL.DTO;
using System;
using System.Collections.Generic;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        void AddNew(PublisherDTO publisherDTO);

        PublisherDTO Get(string companyName);

        IEnumerable<PublisherDTO> GetAll();
    }
}
