using Microsoft.EntityFrameworkCore;
using System;
using VHUX.Api.Entity;
using VHUX.Api.Extensions;
using VHUX.Api.IRepository;
using VHUX.Api.Model.Order;
using VHUX.Model;

namespace VHUX.Api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _context;
        private readonly IPaymentRepository _paymentRepository;
        private readonly VnpayService _vnpayService;
        public OrderRepository(ApplicationContext context, VnpayService vnpayService, IPaymentRepository paymentRepository)
        {
            _context = context;
            _vnpayService = vnpayService;
            _paymentRepository = paymentRepository;
        }
        public async Task<PaginationSet<Order>> OrderList(string? keyword, byte status, int page_number, int page_size)
        {
            return await Task.Run(async () =>
            {
                PaginationSet<Order> response = new PaginationSet<Order>();
                var listItem = _context.Order.Where(x => !x.is_delete);

                if (keyword is not null and not "")
                {
                    listItem = listItem.Where(r => r.customer_name.Contains(keyword) || r.customer_address.Contains(keyword) || r.customer_phone.Contains(keyword));
                }
                if (status != null)
                {
                    listItem = listItem.Where(r => r.status == status);
                }

                if (page_number > 0)
                {
                    response.totalcount = listItem.Select(x => x.id).Count();
                    response.page = page_number;
                    response.maxpage = (int)Math.Ceiling((decimal)response.totalcount / page_size);
                    response.list = await listItem.OrderByDescending(r => r.id).Skip(page_size * (page_number - 1)).Take(page_size).ToListAsync();
                }
                else
                {
                    response.list = await listItem.OrderByDescending(r => r.id).ToListAsync();
                }

                return response;
            });
        }
        public async Task<Order> Order(long id)
        {
            Order order = _context.Order.Where(r => r.id == id && !r.is_delete).FirstOrDefault();
            order.orderDetail = _context.Order_Detail.Where(r => !r.is_delete && r.order_id == id).ToList();
            return order;

        }
        public async Task<bool> OrderDelete(long id)
        {
            return await Task.Run(() =>
            {
                Order order = _context.Order.FirstOrDefault(c => c.id == id);
                order.is_delete = true;
                _context.Order.Update(order);
                _context.SaveChanges();

                return true;
            });
        }
        public async Task<Order> OrderCreate(Order order)
        {
            return await Task.Run(async () =>
            {
                _context.Order.Add(order);
                _context.SaveChanges();
                foreach (var item in order.orderDetail)
                {
                    item.order_id = order.id;
                }
                _context.Order_Detail.AddRange(order.orderDetail);
                string url = "";

                PaymentModel model = new PaymentModel
                {
                    price = order.total_price,
                    status_id = 0,
                    dateAdded = DateTime.Now,
                    vnp_txnref = order.id.ToString(),
                    payment_status_id = order.payment_status_id,
                    order_id = order.id,
                };
                url = await _vnpayService.OrderVnPay(model);
                model.url = url;
                string response = await _paymentRepository.PaymentCreate(model);
                return order;
            });
        }
        public async Task<Order> OrderModify(Order order)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _context.Entry(order).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch
                {
                    order.id = 0;
                }

                return order;
            });
        }
       
    }
}
