using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly IRepository _repository;
        private readonly int _pageSize;

        public PhoneService(IRepository repository)
        {
            _repository = repository;
            _pageSize = 2;
        }

        public async Task<IEnumerable<Phone>> GetPhoneItems(int phonePage)
        {
            var phones = (await _repository.GetPhones())
                .OrderBy(p => p.Id)
                .Skip((phonePage - 1) * _pageSize)
                .Take(_pageSize);
            return phones;
        }

        public async Task<PagingInfo> GetNumberOfPages(int phonePage)
        {
            var pagingInfo = new PagingInfo
            {
                CurrentPage = phonePage,
                ItemsPerPage = _pageSize,
                TotalItems = (await _repository.GetPhones()).Count
            };
            return pagingInfo;
        }
    }
}
