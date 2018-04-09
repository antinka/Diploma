using GameStore.BLL.DTO;
using System;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        void AddNew(PublisherDTO publisherDTO);

        PublisherDTO Get(string companyName);
    }
}
