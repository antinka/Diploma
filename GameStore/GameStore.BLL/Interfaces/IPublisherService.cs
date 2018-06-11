using System.Collections.Generic;
using System;
using GameStore.BLL.DTO;

namespace GameStore.BLL.Interfaces
{
    public interface IPublisherService
    {
        void AddNew(ExtendPublisherDTO publisherDTO);

        ExtendPublisherDTO GetByName(string companyName);

        IEnumerable<PublisherDTO> GetAll();

        void Update(ExtendPublisherDTO publisherDTO);

        void Delete(Guid id);

        bool IsUniqueName(ExtendPublisherDTO publisherDTO);
    }
}
