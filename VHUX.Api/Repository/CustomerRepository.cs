using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using VHUX.Api.Entity;
using VHUX.Api.IRepository;
using VHUX.API.Extensions;
using VHUX.API.Model.Customer;
using VHUX.Model;

namespace VHUX.Api.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public Task<int> CustomerCheck(string phone)
        {
            int check = _context.Customer.Where(r => r.phone == phone).Count();
            return Task.FromResult(check);
        }
        public async Task<Customer> CustomerGetPhone(string phone)
        {
            Customer customer = new Customer();
            customer = _context.Customer.Where(r => r.phone == phone).FirstOrDefault();
            return customer;
        }
        public async Task<string> CustomerCreate(CustomerAddModel model)
        {

            return await Task.Run(() =>
            {
                string respons = "0";
                try
                {
                    Customer customer = new Customer
                    {
                        phone = model.phone,
                        name = model.name
                    };
                    customer.password = Encryptor.MD5Hash(model.password);
                    customer.dateAdded = DateTime.Now;
                    customer.affliate = GetAffiliateCode();
                    if (model.customer_affliate != null)
                    {
                        UpdatePointCustomer(model.customer_affliate);
                    }
                    _context.Customer.Add(customer);
                    _context.SaveChanges();
                    _context.Customer.Update(customer);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    respons = ex.Message;
                }
                return Task.FromResult(respons);
            });

        }
        public async Task<int> Authenticate(CustomerLoginModel login)
        {
            Customer user = await _context.Customer.Where(r => r.phone.ToUpper() == login.username.ToUpper() && !r.is_delete).FirstOrDefaultAsync();

            var passWord = Encryptor.MD5Hash(login.password);
            return passWord != user.password ? 2 : 1;

        }
        public async Task<CustomerModel> Customer(long id)
        {
            return await Task.Run(async () =>
            {
                CustomerModel model = new CustomerModel();
                Customer customer = await _context.Customer.FirstOrDefaultAsync(r => r.id == id && !r.is_delete);
                if (customer != null)
                {
                    model = _mapper.Map<CustomerModel>(customer);

                }

                return model;
            });
        }

        public async Task<CustomerModel> CustomerModify(CustomerModel model)
        {
            return await Task.Run(() =>
            {
                Customer customer = _context.Customer.FirstOrDefault(x => x.id == model.id);
                customer.name = model.name;
                customer.address = model.address;
                customer.phone = model.phone;

                _context.Customer.Update(customer);
                _context.SaveChanges();

                return Task.FromResult(model);
            });
        }
        public async Task<bool> CustomerDelete(long customer_id, long user_id)
        {
            return await Task.Run(() =>
            {
                var customer = _context.Customer.FirstOrDefault(x => x.id == customer_id);
                if (customer != null)
                {
                    customer.is_delete = true;
                    customer.dateUpdated = DateTime.Now;
                    customer.userUpdated = user_id;
                    _context.Customer.Update(customer);
                    _context.SaveChanges();

                }
                return Task.FromResult(true);
            });
        }
        public async Task<PaginationSet<CustomerModel>> CustomerList(string? keyword, int page_number, int page_size)
        {
            await Task.CompletedTask;
            PaginationSet<CustomerModel> response = new PaginationSet<CustomerModel>();
            IQueryable<CustomerModel> listItem = from a in _context.Customer
                                                 where !a.is_delete
                                                 select new CustomerModel
                                                 {
                                                     id = a.id,
                                                     name = a.name,
                                                     phone = a.phone,
                                                     affliate = a.affliate,
                                                     birthday = a.birthday,
                                                     point = a.point,
                                                     address = a.address,
                                                     dateAdded = a.dateAdded,
                                                     userAdded = a.userAdded,
                                                     dateUpdated = a.dateUpdated,
                                                     userUpdated = a.userUpdated,
                                                 };

            if (keyword is not null and not "" && keyword!= "undefined")
            {
                listItem = listItem.Where(r => r.name.Contains(keyword) || r.phone.Contains(keyword));
            }
            if (page_number > 0)
            {
                response.totalcount = listItem.Select(x => x.id).Count();
                response.page = page_number;
                response.maxpage = (int)Math.Ceiling((decimal)response.totalcount /     page_size);
                response.list = await listItem.OrderByDescending(r => r.dateAdded).Skip(page_size * (page_number - 1)).Take(page_size).ToListAsync();
            }
            else
            {
                response.list = await listItem.OrderByDescending(r => r.dateAdded).ToListAsync();
            }
            return response;

        }
        private void UpdatePointCustomer(string affliate_code)
        {
            var customer = _context.Customer.Where(r => r.affliate == affliate_code).FirstOrDefault();
            if (customer != null)
            {
                customer.point = customer.point + 2;
                _context.Customer.Update(customer);
                _context.SaveChanges();
            }
        }
        private string GetAffiliateCode()
        {
            string affliate_code = RandomString.GetAffiliateCode();

            while (true)
            {
                int checkcode = _context.Customer.Where(r => r.affliate == affliate_code && !r.is_delete).Count();
                if (checkcode > 0)
                {
                    affliate_code = RandomString.GetAffiliateCode();
                }
                else
                {
                    break;
                }
            }
            return affliate_code;


        }
        public async Task<string> OTPCreateForLogin(string phone_number)
        {
            return await Task.Run(async () =>
            {
                string mess = "0";

                var user = _context.Customer.FirstOrDefault(r => r.phone == phone_number && !r.is_delete);
                if (user == null)
                {
                    mess = " Số điện thoại của bạn chưa đăng ký sử dụng user trong hệ thống, vui lòng liên hệ quản trị để được hỗ trợ";
                    return mess;
                }
                DateTime day_send = DateTime.Now.Date;
                var sendcheck = _context.SMS_OTP.Where(r => r.phone_number == phone_number && r.day_send == day_send && r.type == 1 && !r.is_delete).Count();
                if (sendcheck >= 5)
                {
                    mess = " Số điện thoại của bạn nhận quá số lượng otp được phép vui lòng kiểm tra lại";
                    return mess;
                }
                string otpsms = Encryptor.OTP();
                SMSExtensions SMS_services = new();
                string content = await SMS_services.SendOTPLogin(phone_number, otpsms);
                SMS_OTP oTP = new SMS_OTP();
                oTP.date_send = DateTime.Now;
                oTP.day_send = day_send;
                oTP.otp = otpsms;
                oTP.type = 1;
                if (content != "0")
                {
                    oTP.content = content;
                    oTP.send_status = true;
                }
                else
                {
                    oTP.content = content;
                    oTP.send_status = false;
                }
                oTP.phone_number = phone_number;
                _context.SMS_OTP.Add(oTP);
                _context.SaveChanges();
                return mess;
            });
        }
        public async Task<string> CheckOTP(string phone_number, string otp)
        {
            return await Task.Run(async () =>
            {
                string mess = "0";
                var sms_otp = _context.SMS_OTP.Where(r => r.phone_number == phone_number).OrderByDescending(r => r.id).FirstOrDefault();

                if (sms_otp == null)
                {
                    mess = "Mã của bạn không chính xác vui lòng kiểm tra lại";
                }
                else
                {
                    if (sms_otp.otp != otp)
                        mess = "Mã của bạn không chính xác vui lòng kiểm tra lại";
                    else
                    {
                        DateTime datenow = DateTime.Now;
                        sms_otp.is_delete = true;
                        _context.SMS_OTP.Update(sms_otp);
                        _context.SaveChanges();
                        if (datenow > sms_otp.date_send.AddMinutes(5))
                            mess = "Mã xác thực của bạn đã quá hạn vui lòng kiểm tra lại";
                    }
                }
                return mess;
            });
        }
        public async Task<bool> CustomerSignature(CustomerSignatureModel model)
        {
            return await Task.Run(() =>
            {
                try
                {
                    Customer customer = _context.Customer.FirstOrDefault(x => x.id == model.id);
                    customer.signature = model.file;
                    _context.Customer.Update(customer);
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            });
        }
    }
}
