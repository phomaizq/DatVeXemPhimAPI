using AutoMapper;
using AutoMapper.Configuration;
using BanVeXemPhimApi.Common;
using BanVeXemPhimApi.Controllers;
using BanVeXemPhimApi.Database;
using BanVeXemPhimApi.Dto;
using BanVeXemPhimApi.Request;
using BanVeXemPhimApi.Services;
using BanVeXemPhimApi.SocketHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderTicketController : BaseApiController<OrderTicketController>
    {
        private readonly OrderTicketService _orderTicketService;
        private readonly ConnectionManager _connectionManager;
        public OrderTicketController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig, IWebHostEnvironment webHost, ConnectionManager connectionManager)
        {
            _orderTicketService = new OrderTicketService(apiConfig, databaseContext, mapper, webHost);
            _connectionManager = connectionManager;
        }

        /// <summary>
        /// Get schedule is playing list
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CompleteBookTicket")]
        public MessageData CompleteBookTicket(BookTicketRequest bookTicketRequest)
        {
            try
            {
                var res = _orderTicketService.CompleteBookTicket(UserId, bookTicketRequest);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }

        /// <summary>
        /// order history
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OrderHistory")]
        public MessageData OrderHistory()
        {
            try
            {
                var res = _orderTicketService.OrderHistory(UserId);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "error", Des = ex.Message };
            }
        }

        /// <summary>
        /// Create PaymentCode
        /// </summary>
        /// <param name="bookTicketRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreatePaymentCode")]
        public MessageData CreatePaymentCode(BookTicketRequest bookTicketRequest)
        {
            try
            {
                var res = _orderTicketService.CreatePaymentCode(UserId, bookTicketRequest);
                return new MessageData { Data = res };
            }
            catch (Exception ex)
            {
                return new MessageData() { Code = "Error", Des = ex.Message };
            }
        }

        /// <summary>
        /// Complete Book Ticket By QRCode
        /// </summary>
        /// <param name="bookTicketRequest"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("CompleteBookTicketByQRCode")]
        public async Task<ContentResult> CompleteBookTicketByQRCodeAsync(int userId, [FromQuery] BookTicketRequest bookTicketRequest)
        {
            try
            {
                var res = _orderTicketService.CompleteBookTicket(userId, bookTicketRequest);
                var connectionList = _connectionManager.GetAllSockets();
                var connection = default(KeyValuePair<string, WebSocket>);
                foreach (var item in connectionList)
                {
                    var user = _connectionManager.GetUsernameBySocket(item.Value);
                    if(user == userId.ToString())
                    {
                        connection = item;
                    }
                }
                if (!connection.Equals(default(KeyValuePair<string, WebSocket>)))
                {
                    var socket = connection.Value;
                    if (socket.State == WebSocketState.Open)
                    {
                        var message = "{\"result\": true}";
                        await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                          offset: 0,
                                                                          count: message.Length),
                                           messageType: WebSocketMessageType.Text,
                                           endOfMessage: true,
                                           cancellationToken: CancellationToken.None);
                    }
                }
                return base.Content("<div style=\"width: 100%; height: 100vh; display: flex; justify-content: center; align-items: center; font-size: 25px;\">You have successfully paid!</div>", "text/html");
            }
            catch (Exception ex)
            {
                return base.Content("<div style=\"width: 100%; height: 100vh; display: flex; justify-content: center; align-items: center; font-size: 25px;\">" + ex.Message + "</div>", "text/html");
            }
        }
    }
}
