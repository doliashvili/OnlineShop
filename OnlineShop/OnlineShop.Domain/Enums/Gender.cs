using System;

namespace OnlineShop.Domain.Enums
{
    [Flags]
    public enum Gender : byte
    {
        Male,
        Female,
        Other
    }
}
