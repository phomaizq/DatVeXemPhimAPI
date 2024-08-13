using AutoMapper;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Repositories;
using BanVeXemPhimApi.Services;
using IronXL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Manager")]
    public class StatictisController : BaseApiController<StatictisController>
    {
        private readonly StatictisService _statictisService;
        private readonly IWebHostEnvironment _webHost;
        private readonly OrderTicketRepository _orderTicketRepository;
        public StatictisController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost)
        {
            _statictisService = new StatictisService(apiConfig, databaseContext, mapper, webHost);
            _orderTicketRepository = new OrderTicketRepository(apiConfig, databaseContext, mapper);
            _webHost = webHost;
        }

        /// <summary>
        /// get user list
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [HttpGet("StatictisOrderTicket")]
        public IActionResult StatictisOrderTicket(DateTime dateFrom, DateTime dateTo, int limit, int page)
        {
            try
            {
                var res = _statictisService.StatictisOrderTicket(dateFrom, dateTo, limit, page);
                return Ok(new MessageData { Data = res });
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        [HttpGet("ExportFile")]
        [AllowAnonymous]
        public async Task<IActionResult> ExportFile(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var orderTicketList = _orderTicketRepository.FindByCondition(row => row.CreatedDate >= dateFrom && row.CreatedDate <= dateTo).ToList();
                var totalMoney = orderTicketList.Sum(row => row.TotalPrice);

                // set file name
                var nameFile = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");

                WorkBook workBook = WorkBook.Create(ExcelFileFormat.XLSX);
                WorkSheet workSheet = workBook.CreateWorkSheet(nameFile);
                workSheet.DisplayGridlines = false;

                workSheet["A1"].Value = "Rạp chiếu phim";
                workSheet["A1"].Style.Font.Bold = true;
                workSheet.Merge("A1:C1");

                workSheet["A2"].Value = "Từ ngày: ";
                workSheet["A2"].Style.Font.Bold = true;
                workSheet["B2"].Value = dateFrom.ToString("dd-MM-yyyy");
                workSheet.Merge("B2:C2");

                workSheet["A3"].Value = "Đến ngày: ";
                workSheet["A3"].Style.Font.Bold = true;
                workSheet["B3"].Value = dateTo.ToString("dd-MM-yyyy");
                workSheet.Merge("B3:C3");

                workSheet.Merge("A4:W6");
                workSheet["A4"].Value = "BÁO CÁO MUA VÉ";
                workSheet["A4"].Style.VerticalAlignment = IronXL.Styles.VerticalAlignment.Center;
                workSheet["A4"].Style.HorizontalAlignment = IronXL.Styles.HorizontalAlignment.Center;
                workSheet["A4"].Style.Font.Bold = true;
                workSheet["A4"].Style.Font.Height = 30;


                workSheet["C9"].Value = "STT";
                workSheet["D9"].Value = "Mã khách hàng";
                workSheet.Merge("D9:E9");
                workSheet["G9"].Value = "Lịch chiếu";
                workSheet.Merge("G9:H9");
                workSheet["I9"].Value = "Khách hàng";
                workSheet.Merge("I9:K9");
                workSheet["L9"].Value = "Số điện thoại";
                workSheet.Merge("L9:M9");
                workSheet["N9"].Value = "Email";
                workSheet.Merge("N9:Q9");
                workSheet["R9"].Value = "Danh sách ghế";
                workSheet.Merge("R9:S9");
                workSheet["T9"].Value = "Tổng tiền";
                workSheet.Merge("T9:U9");

                for (int i = 0; i < orderTicketList.Count; i++)
                {
                    workSheet["C" + (i + 10).ToString()].Value = (i + 1).ToString();

                    workSheet["D" + (i + 10).ToString()].Value = orderTicketList[i].UserId.ToString();
                    workSheet.Merge("D" + (i + 10).ToString() + ":E" + (i + 10).ToString());
                    workSheet["G" + (i + 10).ToString()].Value = orderTicketList[i].ScheduleId.ToString();
                    workSheet.Merge("G" + (i + 10).ToString() + ":H" + (i + 10).ToString());
                    workSheet["I" + (i + 10).ToString()].Value = orderTicketList[i].Name.ToString();
                    workSheet.Merge("I" + (i + 10).ToString() + ":K" + (i + 10).ToString());
                    workSheet["L" + (i + 10).ToString()].Value = orderTicketList[i].NumberPhone.ToString();
                    workSheet.Merge("L" + (i + 10).ToString() + ":M" + (i + 10).ToString());
                    workSheet["N" + (i + 10).ToString()].Value = orderTicketList[i].Email;
                    workSheet.Merge("N" + (i + 10).ToString() + ":Q" + (i + 10).ToString());
                    workSheet["R" + (i + 10).ToString()].Value =  orderTicketList[i].SeatList;
                    workSheet.Merge("R" + (i + 10).ToString() + ":S" + (i + 10).ToString());
                    workSheet["T" + (i + 10).ToString()].Value = orderTicketList[i].TotalPrice.ToString();
                    workSheet.Merge("T" + (i + 10).ToString() + ":U" + (i + 10).ToString());
                }

                workSheet["C" + (orderTicketList.Count + 11)].Value = "Tổng cộng: ";
                workSheet["C" + (orderTicketList.Count + 11)].Style.Font.Bold = true;
                workSheet.Merge("C" + (orderTicketList.Count + 11).ToString() + ":D" + (orderTicketList.Count + 11).ToString());

                workSheet["T" + (orderTicketList.Count + 11).ToString()].Value = totalMoney.ToString();
                workSheet["T" + (orderTicketList.Count + 11).ToString()].Style.Font.Color = "#D40046";
                workSheet.Merge("T" + (orderTicketList.Count + 11).ToString() + ":U" + (orderTicketList.Count + 11).ToString());

                var ulrFile = Directory.GetCurrentDirectory() + "\\FileExcelExport\\" + nameFile + ".xlsx";
                workSheet.SaveAs(ulrFile);

                //return download file
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "FileExcelExport", nameFile + ".xlsx");

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(filepath, out var contenttype))
                {
                    contenttype = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                return File(bytes, contenttype, Path.GetFileName(filepath));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
