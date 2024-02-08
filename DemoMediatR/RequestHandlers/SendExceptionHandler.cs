using MediatR.Pipeline;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMediatR.RequestHandlers
{
    public class SendExceptionHandler : IRequestExceptionHandler<SendRequest, SendResponse>
    {
        public Task Handle(SendRequest request, Exception exception, RequestExceptionHandlerState<SendResponse> state, CancellationToken cancellationToken)
        {
            // **Log the error:**
            Console.Error.WriteLine("SendExceptionHandler.Handle");

            // **Handle specific error conditions:**
            // You can use the exception type or message to identify specific error scenarios and take appropriate actions.
            // For example, you could:
            // - Retry the request with different parameters.
            // - Notify the user about the error and provide instructions.
            // - Redirect the user to a different page or service.

            // **Throw a new exception:**
            // If the error cannot be handled gracefully, you can throw a new exception to indicate a failure.
            // For example, you could throw a custom exception with additional details about the error.

            // **Do nothing:**
            // In some cases, you may choose to do nothing and let the exception propagate.
            // This is typically done for non-critical errors that do not require any special handling.
            return Task.CompletedTask;
        }
    }
}
