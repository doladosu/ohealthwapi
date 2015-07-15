using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace Health.Setup
{
    internal class NoOpModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            return true;
        }
    }
}