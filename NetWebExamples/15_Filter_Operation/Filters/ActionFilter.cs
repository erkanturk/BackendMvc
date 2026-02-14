using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace _15_Filter_Operation.Filters
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //işlem sonrası
            Debug.WriteLine("Action executed");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //İşlem sırası
            Debug.WriteLine("Action executing");
        }
    }
}
