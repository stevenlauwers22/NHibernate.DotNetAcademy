using System.Web.Mvc;
using DotNetAcademy.NhibernateArch.UI.Mvc.Code;

namespace DotNetAcademy.NhibernateArch.UI.Mvc.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AutoCommitTransactionFilter());
        }
    }
}