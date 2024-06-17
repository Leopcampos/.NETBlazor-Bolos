using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazorBolos.Models;

namespace MudBlazorBolos.Components.Pages
{
    public partial class GerenciaBolosDialog
    {
        [CascadingParameter]
        MudDialogInstance? MudDialog { get; set; }

        [Parameter]
        public Bolo bolo { get; set; } = new Bolo();

        private void Cancela()
        {
            MudDialog?.Cancel();
        }

        private void Submit()
        {
            MudDialog?.Close(DialogResult.Ok<Bolo>(bolo));
        }
    }
}