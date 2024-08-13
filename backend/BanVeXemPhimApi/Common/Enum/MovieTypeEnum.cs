using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BanVeXemPhimApi.Common.Enum
{
    public enum MovieTypeEnum : byte
    {
        [Description("Hành động")] Action = 0,
        [Description("Tình cảm")] Romance = 1,
        [Description("Hoạt hình")] Cartoon = 2,
        [Description("Kinh dị")] Horror = 3,
    }
}
