using System.Web.Mvc;
using NHibernate;

namespace DotNetAcademy.NhibernateArch.UI.Mvc.Code
{
    public class AutoCommitTransactionFilter : IActionFilter
    {
        public ISession Session { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Session.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Session.Transaction.Commit();
        }
    }
}
