using MediatR.Pipeline;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMediatR.RequestHandlers
{
    public class SendExceptionAction : IRequestExceptionAction<SendRequest, Exception>
    {
        public Task Execute(SendRequest request, Exception exception, CancellationToken cancellationToken)
        {
            Console.Error.WriteLine("SendExceptionAction.Execute");
            return Task.CompletedTask;
        }
    }
}
