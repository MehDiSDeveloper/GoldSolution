using Gold.Core.Domain.Entities.Finance;
using Gold.Core.Domain.IdentityEntities;
using Gold.Core.DTO.Finance;
using Gold.Core.ServiceContracts;
using Gold.Infrastructure.GoldDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Gold.Infrastructure.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Gold.Core.DTO.AccountDTO;

namespace Gold.Infrastructure.Services
{
    public class GoldServices : IGoldServices
    {
        #region Injections
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public GoldServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        #endregion



        public async Task<List<UserBillDTO>> GetUserBills(HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            List<UserBillDTO> bills1 = await _context.UserCashGolds.Where(u => u.ToAppUser == user).Select(UCG => new UserBillDTO
            {
                Id = UCG.Id,
                TotalCashAmount = UCG.TotalCash,
                SpentCash = UCG.SpentCash,
                LeftCash = UCG.LeftCash,
                IsActive = UCG.IsActive,
                IsDone = UCG.IsDone,
                IsPayed=UCG.IsPayed,
                DateTime = UCG.DateTime,
                Date = DateOnly.FromDateTime(UCG.DateTime),
                ExecutionTime = UCG.ExecutionTime,
                Delay = (TimeSpan.FromTicks(UCG.Delay)).StripMilliseconds(),
                EarnedGold = UCG.EarnedGold,
                Type = "خرید",
                DonePercent = Math.Round(((double)UCG.SpentCash / (double)UCG.TotalCash) * 100, 1)

            }).ToListAsync();

            List<UserBillDTO> bills = await _context.UserGoldCashs.Where(u => u.ToAppUser == user).Select(UGC => new UserBillDTO
            {
                Id = UGC.Id,
                TotalGoldAmount = UGC.TotalGold,
                SpentGold = UGC.SpentGold,
                LeftGold = UGC.LeftGold,
                IsDone = UGC.IsDone,
                IsPayed = UGC.IsPayed,
                EarnedCash = UGC.EarnedCash,
                DateTime = UGC.DateTime,
                Date = DateOnly.FromDateTime(UGC.DateTime),
                ExecutionTime = UGC.ExecutionTime,
                Delay = (TimeSpan.FromTicks(UGC.Delay)).StripMilliseconds(),
                IsActive = UGC.IsActive,
                Type = "فروش",
                DonePercent = Math.Round((UGC.SpentGold / UGC.TotalGold) * 100, 1)
            }).ToListAsync();
            bills.AddRange(bills1);
            bills = bills.OrderByDescending(b => b.DateTime).ToList();
            return bills;
        }

        public async Task<List<UserBillDTO>> GetUsersBills(string status, string type, string searchString)
        {
            List<UserBillDTO> bills = new();
            var bills1 = _context.UserCashGolds.Include(b => b.ToAppUser).OrderByDescending(b => b.DateTime).Select(UCG => new UserBillDTO
            {
                Id = UCG.Id,
                TotalCashAmount = UCG.TotalCash,
                SpentCash = UCG.SpentCash,
                LeftCash = UCG.LeftCash,
                IsActive = UCG.IsActive,
                IsDone = UCG.IsDone,
                Date = DateOnly.FromDateTime(UCG.DateTime),
                DateTime = UCG.DateTime,
                ExecutionTime = UCG.ExecutionTime,
                Delay = (TimeSpan.FromTicks(UCG.Delay)).StripMilliseconds(),
                EarnedGold = UCG.EarnedGold,
                Type = "خرید",
                DonePercent = Math.Round(((double)UCG.SpentCash / (double)UCG.TotalCash) * 100, 1),
                ToAppUser = UCG.ToAppUser

            });
            var bills2 = _context.UserGoldCashs.Include(b => b.ToAppUser).OrderByDescending(b => b.DateTime).Select(UGC => new UserBillDTO
            {
                Id = UGC.Id,
                TotalGoldAmount = UGC.TotalGold,
                SpentGold = UGC.SpentGold,
                EarnedCash = UGC.EarnedCash,
                LeftGold = UGC.LeftGold,
                IsDone = UGC.IsDone,
                Date = DateOnly.FromDateTime(UGC.DateTime),
                DateTime = UGC.DateTime,
                ExecutionTime = UGC.ExecutionTime,
                Delay = (TimeSpan.FromTicks(UGC.Delay)).StripMilliseconds(),
                IsActive = UGC.IsActive,
                Type = "فروش",
                DonePercent = Math.Round((UGC.SpentGold / UGC.TotalGold) * 100, 1),
                ToAppUser = UGC.ToAppUser
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                bills1 = bills1.Where(b => b.ToAppUser.UserName.ToLower().Contains(searchString) || b.ToAppUser.PhoneNumber.Contains(searchString) || b.ToAppUser.FirstName.ToLower().Contains(searchString) || b.ToAppUser.LastName.ToLower().Contains(searchString));
                bills2 = bills2.Where(b => b.ToAppUser.UserName.ToLower().Contains(searchString) || b.ToAppUser.PhoneNumber.Contains(searchString) || b.ToAppUser.FirstName.ToLower().Contains(searchString) || b.ToAppUser.LastName.ToLower().Contains(searchString));
            }
            if (status == "active")
            {
                bills1 = bills1.Where(b => b.IsActive == true);
                bills2 = bills2.Where(b => b.IsActive == true);
            }
            if (status == "suspend")
            {
                bills1 = bills1.Where(b => b.IsActive == false);
                bills2 = bills2.Where(b => b.IsActive == false);
            }
            if (status == "done")
            {
                bills1 = bills1.Where(b => b.IsDone == true);
                bills2 = bills2.Where(b => b.IsDone == true);
            }

            if (type == "buy" || type == "sell")
            {
                if (type == "buy")
                {
                    bills = await bills1.ToListAsync();
                }
                if (type == "sell")
                {
                    bills = await bills2.ToListAsync();
                }
            }
            else
            {
                bills = await bills1.ToListAsync();
                bills.AddRange(bills2);
            }



            bills = bills.OrderByDescending(b => b.DateTime).ToList();
            return bills;
        }

        public async Task<List<SystemBillDTO>> GetSysBills(string status, string type, string searchString)
        {
            List<SystemBillDTO> bills = new();

            var bills1 = _context.SystemCashToGolds.Select(SCG => new SystemBillDTO
            {
                Id = SCG.Id,
                TotalCashAmount = SCG.TotalCash,
                UnitPrice = SCG.UnitPrice,
                SpentCash = SCG.SpentCash,
                LeftCash = SCG.TotalCash - SCG.SpentCash,
                IsActive = SCG.IsActive,
                IsDone = SCG.IsDone,
                Date = DateOnly.FromDateTime(SCG.DateTime),
                DateTime = SCG.DateTime,
                ExecutionTime = SCG.ExecutionTime,
                Delay = SCG.Delay,
                EarnedGold = SCG.EarnedGold,
                Type = "خرید",
                ToAppUser = SCG.ToAppUser,
                DonePercent = Math.Round(((double)SCG.SpentCash / (double)SCG.TotalCash) * 100, 1)

            });
            var bills2 = _context.SystemGoldToCashs.Select(SGC => new SystemBillDTO
            {
                Id = SGC.Id,
                TotalGoldAmount = SGC.TotalGold,
                TotalCashAmount = SGC.TotalCash,
                UnitPrice = SGC.UnitPrice,
                SpentGold = SGC.SoldGold,
                EarnedCash = SGC.EarnedCash,
                LeftGold = SGC.TotalGold - SGC.SoldGold,
                IsDone = SGC.IsDone,
                Date = DateOnly.FromDateTime(SGC.DateTime),
                DateTime = SGC.DateTime,
                ExecutionTime = SGC.ExecutionTime,
                Delay = SGC.Delay,
                IsActive = SGC.IsActive,
                Type = "فروش",
                ToAppUser = SGC.ToAppUser,
                DonePercent = Math.Round((SGC.SoldGold / SGC.TotalGold) * 100, 1)
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                bills1 = bills1.Where(b => b.ToAppUser.UserName.ToLower().Contains(searchString) || b.ToAppUser.PhoneNumber.Contains(searchString) || b.ToAppUser.FirstName.ToLower().Contains(searchString) || b.ToAppUser.LastName.ToLower().Contains(searchString));
                bills2 = bills2.Where(b => b.ToAppUser.UserName.ToLower().Contains(searchString) || b.ToAppUser.PhoneNumber.Contains(searchString) || b.ToAppUser.FirstName.ToLower().Contains(searchString) || b.ToAppUser.LastName.ToLower().Contains(searchString));
            }

            if (status == "active")
            {
                bills1 = bills1.Where(b => b.IsActive == true);
                bills2 = bills2.Where(b => b.IsActive == true);
            }
            if (status == "suspend")
            {
                bills1 = bills1.Where(b => b.IsActive == false);
                bills2 = bills2.Where(b => b.IsActive == false);
            }
            if (status == "done")
            {
                bills1 = bills1.Where(b => b.IsDone == true);
                bills2 = bills2.Where(b => b.IsDone == true);
            }

            if (type == "buy" || type == "sell")
            {
                if (type == "buy")
                {
                    bills = await bills1.ToListAsync();
                }
                if (type == "sell")
                {
                    bills = await bills2.ToListAsync();
                }
            }
            else
            {
                bills = await bills1.ToListAsync();
                bills.AddRange(bills2);
            }

            bills = bills.OrderByDescending(b => b.DateTime).ToList();
            return bills;
        }


        public async Task<double> GetUserAssets(HttpContext httpContext)
        {
            ApplicationUser user = await _userManager.GetUserAsync(httpContext.User);
            return await _context.UserGoldAssets.Where(uga => uga.ToAppUser == user).Select(uga => uga.GoldAmount).FirstOrDefaultAsync();
        }

        public async Task<List<UserCashToGoldDTO>> GetusersGoldDemands()
        {
            return await _context.UserCashGolds.Select(ucg => new UserCashToGoldDTO
            {
                CashAmount = ucg.TotalCash,
                CashExecutedAmount = ucg.SpentCash,
                Id = ucg.Id,
                IsActive = ucg.IsActive,
                IsDone = ucg.IsDone,
                IsPayed = ucg.IsPayed,
                User = ucg.ToAppUser,
            }).ToListAsync();
        }

        public async Task<List<UserGoldToCashDTO>> GetusersGoldOffers()
        {
            return await _context.UserGoldCashs.Select(ugc => new UserGoldToCashDTO
            {
                GoldAmount = ugc.TotalGold,
                GoldExecutedAmount = ugc.SpentGold,
                Id = ugc.Id,
                IsActive = ugc.IsActive,
                IsDone = ugc.IsDone,
                IsPayed = ugc.IsPayed,
                User = ugc.ToAppUser,
            }).ToListAsync();
        }

        public async Task<bool> SetSystemCashToGold(SystemCashToGoldDTO sysC2GDto, HttpContext httpContext)
        {
            ApplicationUser user = await _userManager.GetUserAsync(httpContext.User);
            SystemCashToGold syUCG = new()
            {
                TotalCash = sysC2GDto.TotalCash,
                UnitPrice = sysC2GDto.GoldUnitPrice,
                IsActive = true,
                IsDone = false,
                DateTime = DateTime.Now,
                EarnedGold = 0,
                SpentCash = 0,
                ToAppUser = user,
                LeftCash = sysC2GDto.TotalCash,
            };
            await _context.SystemCashToGolds.AddAsync(syUCG);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetSystemGoldToCash(SystemGoldToCashDTO sysG2CDto, HttpContext httpContext)
        {
            ApplicationUser user = await _userManager.GetUserAsync(httpContext.User);
            SystemGoldToCash sysGC = new()
            {
                TotalCash = sysG2CDto.TotalBoughtPrice,
                TotalGold = sysG2CDto.GoldAmount,
                LeftGold = sysG2CDto.GoldAmount,
                SoldGold = 0,
                IsActive = true,
                UnitPrice = (uint)((double)sysG2CDto.TotalBoughtPrice / sysG2CDto.GoldAmount),
                LeftCash = sysG2CDto.TotalBoughtPrice,
                IsDone = false,
                EarnedCash = 0,
                DateTime = DateTime.Now,
                ToAppUser = user,
            };
            await _context.SystemGoldToCashs.AddAsync(sysGC);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetUserCashToGold(UserCashToGoldDTO uC2GDto, HttpContext httpContext)
        {
            var user = await _userManager.GetUserAsync(httpContext.User);
            if (user == null)
            {
                return false;
            }
            UserCashToGold UCG = new()
            {
                Id = uC2GDto.Id,
                TotalCash = uC2GDto.CashAmount,
                IsActive = true,
                IsPayed = false,
                IsDone = false,
                ToAppUser = user,
                SpentCash = 0,
                LeftCash = uC2GDto.CashAmount,
                DateTime = DateTime.Now,
                EarnedGold = 0
            };
            await _context.UserCashGolds.AddAsync(UCG);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetUserGoldToCash(UserGoldToCashDTO uG2CDto, HttpContext httpContext)
        {
            ApplicationUser user = await _userManager.GetUserAsync(httpContext.User);

            UserGoldToCash UGC = new()
            {
                Id = uG2CDto.Id,
                TotalGold = uG2CDto.GoldAmount,
                LeftGold = uG2CDto.GoldAmount,
                SpentGold = 0,
                IsActive = true,
                IsPayed = true,
                IsDone = false,
                ToAppUser = user,
                DateTime = DateTime.Now,
                EarnedCash = 0,
            };
            await _context.UserGoldCashs.AddAsync(UGC);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> SwitchSusspendUserBuyBill(string Id)
        {
            var bill = await _context.UserCashGolds.SingleOrDefaultAsync(b => b.Id == Id);
            string result = "";
            if (bill != null)
            {
                if (bill.IsDone == true)
                {
                    bill.IsActive = false;
                    result = "این تراکنش پایان یافته و امکان تغییر وضعیت ندارد";
                }
                else if (bill.IsActive)
                {
                    bill.IsActive = false;
                    result = "تراکنش مورد نظر غیر فعال شد";
                }
                else
                {
                    bill.IsActive = true;
                    result = "تراکنش مورد نظر فعال شد";
                }
                await _context.SaveChangesAsync();
                return result;
            }
            return "قبض یافت نشد";
        }

        public async Task<string> SwitchSusspendUserSellBill(string Id)
        {
            var bill = await _context.UserGoldCashs.SingleOrDefaultAsync(b => b.Id == Id);
            string result = "";
            if (bill != null)
            {
                if (bill.IsDone == true)
                {
                    bill.IsActive = false;
                    result = "این تراکنش پایان یافته و امکان تغییر وضعیت ندارد";
                }
                else if (bill.IsActive)
                {
                    bill.IsActive = false;
                    result = "تراکنش مورد نظر غیر فعال شد";
                }
                else
                {
                    bill.IsActive = true;
                    result = "تراکنش مورد نظر فعال شد";
                }
                _context.SaveChanges();
                return result;
            }
            return "قبض یافت نشد";
        }

        public async Task<string> SwitchSusspendSysBuyBill(string Id)
        {
            var bill = await _context.SystemCashToGolds.SingleOrDefaultAsync(b => b.Id == Id);
            string result = "";
            if (bill != null)
            {
                if (bill.IsDone == true)
                {
                    bill.IsActive = false;
                    result = "این تراکنش پایان یافته و امکان تغییر وضعیت ندارد";
                }
                else if (bill.IsActive)
                {
                    bill.IsActive = false;
                    result = "تراکنش مورد نظر غیر فعال شد";
                }
                else
                {
                    bill.IsActive = true;
                    result = "تراکنش مورد نظر فعال شد";
                }
                _context.SaveChanges();
                return result;

            }
            return "قبض یافت نشد";
        }

        public async Task<string> SwitchSusspendSysSellBill(string Id)
        {
            var bill = await _context.SystemGoldToCashs.SingleOrDefaultAsync(b => b.Id == Id);
            string result = "";
            if (bill != null)
            {
                if (bill.IsDone == true)
                {
                    bill.IsActive = false;
                    result = "این تراکنش پایان یافته و امکان تغییر وضعیت ندارد";
                }
                else if (bill.IsActive)
                {
                    bill.IsActive = false;
                    result = "تراکنش مورد نظر غیر فعال شد";
                }
                else
                {
                    bill.IsActive = true;
                    result = "تراکنش مورد نظر فعال شد";
                }
                _context.SaveChanges();
                return result;
            }
            return "قبض یافت نشد";
        }

        #region Execution bills logics
        public async Task<string> ExecuteSysSellbill(string Id)
        {
            int executedBillsCount = 0;
            var sysBill = await _context.SystemGoldToCashs.FirstOrDefaultAsync(sb => sb.Id == Id && sb.TotalGold > sb.SoldGold);
            string result = "";
            if (sysBill == null || sysBill.IsActive == false)
                return "فاکتور سیستمی فعالی یافت نشد";

            List<UserCashToGold> userBills = await _context.UserCashGolds
                .Where(u => u.IsActive == true && u.IsDone == false
                && u.SpentCash < u.TotalCash).OrderBy(u => u.DateTime).ToListAsync();

            if (userBills.Count == 0)
                return "متقاضی فعالی یافت نشد";
            sysBill.LeftGold = sysBill.TotalGold - sysBill.SoldGold;
            sysBill.LeftCash = (uint)(sysBill.LeftGold * (double)sysBill.UnitPrice);
            if (sysBill.LeftCash <= 0)
                return "فاکتور مورد نظر مقداری برای عرضه ندارد";

            foreach (var userBill in userBills)
            {

                decimal deff = userBill.TotalCash - userBill.SpentCash;
                if (deff <= sysBill.LeftCash)
                {
                    double baughtGold = (double)deff / (double)sysBill.UnitPrice;
                    userBill.SpentCash = userBill.TotalCash;
                    userBill.LeftCash = 0;
                    userBill.IsDone = true;
                    userBill.IsActive = false;
                    userBill.EarnedGold += Math.Round(baughtGold, 4);
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;
                    sysBill.EarnedCash += deff;
                    sysBill.LeftCash -= deff;
                    sysBill.SoldGold = Math.Round(sysBill.SoldGold + baughtGold, 4);
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;
                    if (sysBill.SoldGold >= sysBill.TotalGold)
                    {
                        sysBill.IsDone = true;
                        sysBill.IsActive = false;
                    }
                    UserSystemCashToGoldBill uscg = new()
                    {
                        CashAmount = deff,
                        GoldAmount = Math.Round(baughtGold, 4),
                        ToSystemGoldToCash = sysBill,
                        ToUserCashToGold = userBill,
                    };
                    await _context.UserSystemCashToGoldBills.AddAsync(uscg);
                    sysBill.ToUserSystemCashToGoldBills.Add(uscg);
                    userBill.ToUserSystemCashToGoldBills.Add(uscg);
                    await _context.SaveChangesAsync();
                    executedBillsCount += 1;
                    deff = 0;
                    result = $"تعداد {executedBillsCount} فاکتور تغذیه شد ";
                }

                else if (deff > sysBill.LeftCash && sysBill.LeftCash > 0)
                {
                    double baughtGold =Convert.ToDouble(sysBill.LeftCash / sysBill.UnitPrice);
                    userBill.SpentCash += sysBill.LeftCash;
                    userBill.IsDone = false;
                    userBill.IsActive = true;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;
                    sysBill.EarnedCash += sysBill.LeftCash;
                    sysBill.SoldGold = Math.Round(sysBill.SoldGold + baughtGold, 4);
                    if (sysBill.SoldGold >= sysBill.TotalGold)
                    {
                        sysBill.IsDone = true;
                        sysBill.IsActive = false;
                    }
                    UserSystemCashToGoldBill uscg = new()
                    {
                        CashAmount = sysBill.LeftCash,
                        GoldAmount = Math.Round(baughtGold, 4),
                        ToSystemGoldToCash = sysBill,
                        ToUserCashToGold = userBill,
                    };
                    sysBill.LeftCash = 0;
                    sysBill.LeftGold = 0;
                    await _context.UserSystemCashToGoldBills.AddAsync(uscg);
                    userBill.ToUserSystemCashToGoldBills.Add(uscg);
                    sysBill.ToUserSystemCashToGoldBills.Add(uscg);
                    await _context.SaveChangesAsync();

                    executedBillsCount += 1;
                    result = $"تعداد {executedBillsCount} فاکتور تغذیه شد ";
                    break;
                }

                else if (sysBill.LeftCash <= 0)
                {
                    result = $"تعداد {executedBillsCount} فاکتور تغذیه شد ";
                    break;
                }
            }
            return result;
        }

        public async Task<string> ExecuteSysBuybill(string Id)
        {
            var sysBill = await _context.SystemCashToGolds.FirstOrDefaultAsync(sb => sb.Id == Id && sb.TotalCash > sb.SpentCash);
            string result = "";
            if (sysBill == null || sysBill.IsActive == false)
                return "فاکتور سیستمی فعالی یافت نشد";

            var userBills = await _context.UserGoldCashs
                .Where(u => u.IsActive == true && u.IsDone == false)
                .OrderBy(ub => ub.DateTime).ToListAsync();

            if (userBills.Count == 0)
                return "متقاضی فعالی یافت نشد";

            if (sysBill.LeftCash <= 0)
                return "فاکتور مورد نظر مقداری برای عرضه ندارد";

            int executedBillsCount = 0;

            foreach (var userBill in userBills)
            {
                userBill.LeftGold = userBill.TotalGold - userBill.SpentGold;
                //price of user left gold ,ready to sell
                decimal deff = (decimal)(userBill.LeftGold) * sysBill.UnitPrice;
                sysBill.LeftCash = sysBill.TotalCash - sysBill.SpentCash;

                if (deff > sysBill.LeftCash && sysBill.LeftCash > 0)
                {
                    var dealGold = Math.Round(((double)sysBill.LeftCash / (double)sysBill.UnitPrice), 4);
                    var dealCash = sysBill.LeftCash;

                    userBill.EarnedCash += sysBill.LeftCash;
                    userBill.LeftGold -= dealGold;
                    userBill.SpentGold += dealGold;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;

                    sysBill.SpentCash = sysBill.TotalCash;
                    sysBill.EarnedGold += dealGold;
                    sysBill.IsActive = false;
                    sysBill.IsDone = true;
                    sysBill.LeftCash = 0;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;

                    UserSystemGoldToCashBill uscg = new()
                    {
                        CashAmount = dealCash,
                        GoldAmount = dealGold,
                        ToSystemCashToGold = sysBill,
                        ToUserGoldToCash = userBill,
                    };
                    await _context.UserSystemGoldToCashBills.AddAsync(uscg);
                    await _context.SaveChangesAsync();
                    executedBillsCount += 1;
                    result = $"تعداد {executedBillsCount} فاکتور تغذیه شد ";
                    break;
                }

                if (deff <= sysBill.LeftCash)
                {
                    var dealGold = userBill.LeftGold;

                    userBill.SpentGold = userBill.TotalGold;
                    userBill.IsDone = true;
                    userBill.IsActive = false;
                    userBill.EarnedCash += deff;
                    userBill.LeftGold = 0;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;

                    sysBill.SpentCash += deff;
                    sysBill.LeftCash -= deff;
                    sysBill.EarnedGold += userBill.LeftGold;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;

                    UserSystemGoldToCashBill uscg = new()
                    {
                        CashAmount = deff,
                        GoldAmount = dealGold,
                        ToSystemCashToGold = sysBill,
                        ToUserGoldToCash = userBill,
                    };
                    await _context.UserSystemGoldToCashBills.AddAsync(uscg);
                    await _context.SaveChangesAsync();
                    executedBillsCount += 1;
                }

                if (sysBill.LeftCash <= 49)
                {
                    sysBill.IsActive = false;
                    sysBill.IsDone = true;
                    sysBill.LeftCash = 0;
                    sysBill.SpentCash = sysBill.TotalCash;
                    result = $"تعداد {executedBillsCount} فاکتور تغذیه شد ";
                    break;
                }
            }
            result = $"تعداد {executedBillsCount} فاکتور تغذیه شد ";
            return result;
        }

        public async Task<string> ExecuteUserSellBill(string Id)
        {
            var userBill = await _context.UserGoldCashs.Include(u => u.ToAppUser).FirstOrDefaultAsync(ub => ub.Id == Id);
            string result = "";
            if (userBill == null || userBill.IsActive == false)
                return "فاکتور سیستمی فعالی یافت نشد";

            if (userBill.TotalGold <= userBill.SpentGold)
            {
                userBill.SpentGold = userBill.TotalGold;
                userBill.IsActive = false;
                userBill.IsDone = true;
                await _context.SaveChangesAsync();
                return "فاکتور مورد نظر مقداری برای عرضه ندارد";
            }
            var sysBills = await _context.SystemCashToGolds
                .Where(u => u.IsActive == true && u.IsDone == false && u.LeftCash > 0).OrderBy(ub => ub.DateTime).Include(s => s.ToAppUser).ToListAsync();
            if (sysBills.Count == 0)
                return "مقداری برای تغذیه وجود ندارد";
            foreach (var sysBill in sysBills)
            {
                uint deff = (uint)(userBill.LeftGold * (double)sysBill.UnitPrice);
                sysBill.LeftCash = sysBill.TotalCash - sysBill.SpentCash;

                if (sysBill.LeftCash > deff)
                {
                    double dealGold = userBill.LeftGold;
                    userBill.IsDone = true;
                    userBill.IsActive = false;
                    userBill.EarnedCash += deff;
                    userBill.SpentGold = userBill.TotalGold;
                    userBill.LeftGold = 0;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;
                    sysBill.SpentCash += deff;
                    sysBill.LeftCash -= deff;
                    sysBill.EarnedGold += userBill.LeftGold;

                    UserSystemGoldToCashBill Usgc = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CashAmount = deff,
                        GoldAmount = dealGold,
                        ToSystemCashToGold = sysBill,
                        ToUserGoldToCash = userBill
                    };
                    var check = await _context.UserSystemGoldToCashBills.AddAsync(Usgc);
                    await _context.SaveChangesAsync();
                    return "فاکتور مورد نظر تغذیه شد و به اتمام رسید";
                }
                else if (sysBill.LeftCash < deff)
                {
                    var dealGold = Math.Round(((double)sysBill.LeftCash / (double)sysBill.UnitPrice), 4);
                    var dealCash = sysBill.LeftCash;

                    userBill.EarnedCash += sysBill.LeftCash;
                    userBill.SpentGold += dealGold;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;
                    userBill.LeftGold = userBill.TotalGold - userBill.SpentGold;

                    sysBill.IsDone = true;
                    sysBill.IsActive = false;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;
                    sysBill.EarnedGold += dealGold;
                    sysBill.LeftCash = 0;

                    UserSystemGoldToCashBill USGC = new()
                    {
                        CashAmount = dealCash,
                        GoldAmount = dealGold,
                        ToSystemCashToGold = sysBill,
                        ToUserGoldToCash = userBill
                    };
                    await _context.UserSystemGoldToCashBills.AddAsync(USGC);
                    await _context.SaveChangesAsync();
                    result = "ناتمام";
                }
                else if (sysBill.LeftCash == deff)
                {
                    var dealGold = userBill.LeftGold;
                    userBill.IsDone = true;
                    userBill.IsActive = false;
                    userBill.EarnedCash += deff;
                    userBill.SpentGold = userBill.TotalGold;
                    userBill.LeftGold = 0;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;

                    sysBill.IsActive = false;
                    sysBill.EarnedGold += userBill.LeftGold;
                    sysBill.IsDone = true;
                    sysBill.SpentCash = sysBill.TotalCash;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;
                    sysBill.LeftCash = 0;

                    UserSystemGoldToCashBill USGC = new()
                    {
                        CashAmount = deff,
                        GoldAmount = dealGold,
                        ToSystemCashToGold = sysBill,
                        ToUserGoldToCash = userBill
                    };
                    await _context.UserSystemGoldToCashBills.AddAsync(USGC);
                    await _context.SaveChangesAsync();
                    return "فاکتور مورد نظر تغذیه شد و به اتمام رسید";
                }
            }
            return result;
        }

        public async Task<string> ExecuteUserBuyBill(string Id)
        {
            var userBill = await _context.UserCashGolds.FirstOrDefaultAsync(ub => ub.Id == Id);
            string result = "";
            if (userBill == null || userBill.IsActive == false)
                return "فاکتور سیستمی فعالی یافت نشد";

            if (userBill.TotalCash <= userBill.SpentCash)
            {
                userBill.SpentCash = userBill.TotalCash;
                userBill.IsActive = false;
                userBill.IsDone = true;
                await _context.SaveChangesAsync();
                return "فاکتور مورد نظر مقداری برای عرضه ندارد";
            }
            var sysBills = await _context.SystemGoldToCashs
                .Where(u => u.IsActive == true && u.IsDone == false
                && u.LeftGold > 0).OrderBy(ub => ub.DateTime).ToListAsync();
            if (sysBills.Count == 0)
                return "مقداری برای تغذیه وجود ندارد";
            foreach (var sysBill in sysBills)
            {
                sysBill.LeftCash = sysBill.TotalCash - sysBill.EarnedCash;
                if (sysBill.LeftCash > userBill.LeftCash)
                {
                    var dealGold = Math.Round(((double)userBill.LeftCash / (double)sysBill.UnitPrice), 4);
                    var dealCash = userBill.LeftCash;

                    userBill.IsDone = true;
                    userBill.IsActive = false;
                    userBill.EarnedGold += dealGold;
                    userBill.LeftCash = 0;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;
                    userBill.SpentCash = userBill.TotalCash;

                    sysBill.SoldGold += dealGold;
                    sysBill.LeftGold = sysBill.TotalGold - sysBill.SoldGold;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;
                    sysBill.EarnedCash += userBill.LeftCash;
                    sysBill.LeftCash = sysBill.TotalCash - sysBill.EarnedCash;

                    UserSystemCashToGoldBill USCG = new()
                    {
                        CashAmount = dealCash,
                        GoldAmount = dealGold,
                        ToSystemGoldToCash = sysBill,
                        ToUserCashToGold = userBill
                    };
                    await _context.UserSystemCashToGoldBills.AddAsync(USCG);
                    await _context.SaveChangesAsync();
                    return "فاکتور مورد نظر تغذیه شد و به اتمام رسید";
                }
                else if (sysBill.LeftCash < userBill.LeftCash)
                {
                    var dealCash = (uint)(sysBill.LeftGold * (double)sysBill.UnitPrice);
                    var dealGold = sysBill.LeftGold;

                    userBill.EarnedGold += sysBill.LeftGold;
                    userBill.SpentCash += dealCash;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;
                    userBill.LeftCash = userBill.TotalCash - userBill.SpentCash;

                    sysBill.IsDone = true;
                    sysBill.IsActive = false;
                    sysBill.LeftCash = 0;
                    sysBill.EarnedCash += dealCash;
                    sysBill.SoldGold += sysBill.TotalGold;
                    sysBill.LeftGold = 0;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;

                    UserSystemCashToGoldBill USCG = new()
                    {
                        CashAmount = dealCash,
                        GoldAmount = dealGold,
                        ToSystemGoldToCash = sysBill,
                        ToUserCashToGold = userBill
                    };
                    await _context.UserSystemCashToGoldBills.AddAsync(USCG);
                    await _context.SaveChangesAsync();
                    result = "ناتمام";
                }
                else if (sysBill.LeftCash == userBill.LeftCash)
                {
                    var dealCash = (uint)(sysBill.LeftGold * (double)sysBill.UnitPrice);
                    var dealGold = sysBill.LeftGold;

                    userBill.IsDone = true;
                    userBill.IsActive = false;
                    userBill.SpentCash = dealCash;
                    userBill.EarnedGold += sysBill.LeftGold;
                    userBill.ExecutionTime = DateTime.Now;
                    userBill.Delay = (userBill.ExecutionTime - userBill.DateTime).Ticks;

                    sysBill.IsDone = true;
                    sysBill.IsActive = false;
                    sysBill.SoldGold = sysBill.TotalGold;
                    sysBill.EarnedCash = dealCash;
                    sysBill.LeftGold = 0;
                    sysBill.LeftCash = 0;
                    sysBill.ExecutionTime = DateTime.Now;
                    sysBill.Delay = (sysBill.ExecutionTime - sysBill.DateTime).Ticks;


                    UserSystemCashToGoldBill USCG = new()
                    {
                        CashAmount = dealCash,
                        GoldAmount = dealGold,
                        ToSystemGoldToCash = sysBill,
                        ToUserCashToGold = userBill
                    };
                    await _context.UserSystemCashToGoldBills.AddAsync(USCG);
                    await _context.SaveChangesAsync();
                    return "فاکتور مورد نظر تغذیه شد و به اتمام رسید";
                }
            }
            return result;
        }


        #endregion
    }
}
