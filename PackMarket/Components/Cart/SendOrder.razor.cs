using Blazorise;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity.UI.Services;

using PackMarket.Services;

using System;
using System.Collections.Generic;
using System.Linq;

namespace PackMarket.Components.Cart
{
    public partial class SendOrder : ComponentBase
    {
        [Inject] public IEmailSender EmailSender { get; set; }
        public Modal? ModalRef;
        Task ShowModal()
        {
            if (ModalRef != null)
            { return ModalRef.Show(); }
            else { return Task.CompletedTask; }
        }
        Task HideModal()
        {
            if (ModalRef != null)
            { return ModalRef.Hide(); }
            else { return Task.CompletedTask; }
        }
        private async Task SendEmailAsync()
        {
            await EmailSender.SendEmailAsync("ruspak7@mail.ru", "Тест", "Тестовое сообщение");
            await HideModal();
        }
    }
}
