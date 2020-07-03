using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneStore.Models;
using PhoneStore.ViewModels;

namespace PhoneStore.Services
{
    public interface IPhoneService
    {
        Task<List<Phone>> GetPhoneItems(int phonePage);
        Task<PagingInfo> GetNumberOfPages(int phonePage);
    }
}