using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;

namespace ExpenseTrackerWebApi.Helpers
{
    public class DateTimeModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider
                .GetValue(bindingContext.ModelName);

            ModelState modelState = new ModelState { Value = valueResult };

            object actualValue = null;

            if (valueResult.AttemptedValue != string.Empty)
            {
                try
                {
                    //actualValue = Convert.ToDateTime(valueResult.AttemptedValue, CultureInfo.InvariantCulture);
                    actualValue = DateTime.ParseExact(valueResult.AttemptedValue, "dd/MM/yyyy", null);
                }
                catch (FormatException e)
                {
                    modelState.Errors.Add(e);
                }
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            return actualValue;
        }
    }
}