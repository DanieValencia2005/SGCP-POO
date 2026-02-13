using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SGCP_POO.Models
{
    public class RequireStudentSessionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var permiteAnonimo = context.ActionDescriptor.EndpointMetadata
     .Any(m => m is AllowAnonymousAttribute);

            if (permiteAnonimo)
                return;
            var session = context.HttpContext.Session;
            var idEstudiante = session.GetInt32("IdEstudiante");
            var rol = session.GetString("Rol");

            if (idEstudiante == null || rol != "Estudiante")
            {
                context.Result = new RedirectToActionResult(
                    "Index",
                    "Login",
                    null
                );
            }

            base.OnActionExecuting(context);
        }
    }
}
