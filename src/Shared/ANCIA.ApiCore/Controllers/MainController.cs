using ANCIA.ApiCore.Controllers.Models;
using ANCIA.Core.Notifications;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace ANCIA.ApiCore.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected DefaultResult CreateSuccessEmptyReturn()
        {
            return new DefaultResult(true);
        }

        protected DefaultResult CreateSuccessReturn(object data)
        {
            return DefaultResult.Ok(data);
        }

        protected DefaultResult CreateErrorReturn(IEnumerable<string> erros)
        {
            return DefaultResult.Fail(erros);
        }

        protected IEnumerable<string> getErrorList(ValidationResult result)
        {
            return result.Errors
                    .Select(e => e.ErrorMessage);
        }

        protected IEnumerable<string> getErrorList(IEnumerable<Notification> notifications)
        {
            return notifications.Select(notification => notification.Message)
                .ToList();
        }
    }
}
