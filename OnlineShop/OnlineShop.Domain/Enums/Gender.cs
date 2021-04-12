using System;

namespace OnlineShop.Domain.Enums
{
    [Flags]
    public enum Gender : byte
    {
        Male = 1,
        Female = 2,
        Other = 3,
    }
}
