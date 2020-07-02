using System.Collections.Generic;
using System.Threading.Tasks;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public interface IPhoneService
    {
        Task<IEnumerable<Phone>> GetPhoneItems(int phonePage);
        Task<PagingInfo> GetNumberOfPages(int phonePage);
    }
}