using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Core.Hubs
{
    public class UserHub: Hub
    {
        public static int TotalViews { get; set; } = 0;
        public async Task NewWindowLoaded()
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }
    }
}
