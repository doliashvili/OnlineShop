using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace OnlineShop.UI.Shared.Common
{
    public partial class ErrorsList
    {
        [Parameter]
        public bool ShowErrors { get; set; }

        [Parameter]
        public IEnumerable<string> Errors { get; set; }

        private void Reset() => this.ShowErrors = !this.ShowErrors;
    }
}