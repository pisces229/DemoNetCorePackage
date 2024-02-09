using MediatR.Pipeline;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoMediatR.RequestHandlers
{
    public class LoggingExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TRequest : IRequest<TResponse>
        where TException : Exception
    {
        //public LoggingExceptionHandler()
        //{
        //}
        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            // "Something went wrong while handling request of type {@requestType}", typeof(TRequest)

            Console.Error.WriteLine("LoggingExceptionHandler.Handle");

            // TODO: when we want to show the user somethig went wrong, we need to expand this with something like
            // a ResponseBase where we wrap the actual response and return an indication whether the call was successful or not.
            state.SetHandled(default!);

            return Task.CompletedTask;
        }
    }
}
