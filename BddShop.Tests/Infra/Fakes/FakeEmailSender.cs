using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BddShop.Infra.Adapters;
using Microsoft.Extensions.DependencyInjection;

namespace BddShop.Tests.Infra.Fakes
{
    public class FakeEmailSender : IEmailSender
    {
        private static ConcurrentDictionary<string,SendEmailInput> _store = new ConcurrentDictionary<string, SendEmailInput>();

        public ValueTask SendAsync(SendEmailInput input)
        {
            _store.TryAdd($"{input.To}:{input.Subject}:{input.TemplateName}", input);
            return new ValueTask();
        }

        internal bool IsEmailSent(SendEmailInput input) =>
            _store.ContainsKey($"{input.To}:{input.Subject}:{input.TemplateName}");
    }

    public static class FakeEmailSenderExtensions
    {
        public static bool IsEmailSent(this IServiceProvider source, SendEmailInput input)
        {
            return ((FakeEmailSender) source.GetService<IEmailSender>()).IsEmailSent(input);
        }
    }
}
