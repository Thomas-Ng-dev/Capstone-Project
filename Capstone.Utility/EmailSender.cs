﻿using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Utility
{
    public class EmailSender : IEmailSender
    {
        // No implementation yet, created this just to avoid exception 
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // TODO logic to send email, placeholder
            return Task.CompletedTask;
        }
    }
}
