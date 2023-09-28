using VHUX.Api.Entity;
using VHUX.API.Model.Customer;
using VHUX.Model;

namespace VHUX.Api.IRepository
{
    public interface ICustomerRepository
    {
        Task<int> CustomerCheck(string phone);
        Task<Customer> CustomerGetPhone(string phone);
        Task<string> CustomerCreate(CustomerAddModel model);
        Task<int> Authenticate(CustomerLoginModel login);
        Task<CustomerModel> Customer(long id);
        Task<CustomerModel> CustomerModify(CustomerModel model);
        Task<bool> CustomerSignature(CustomerSignatureModel model);
        Task<string> CheckOTP(string phone_number, string otp);
        Task<bool> CustomerDelete(long customer_id, long user_id);
        Task<PaginationSet<CustomerModel>> CustomerList(string? keyword, int page_number, int page_size);
    }
}
