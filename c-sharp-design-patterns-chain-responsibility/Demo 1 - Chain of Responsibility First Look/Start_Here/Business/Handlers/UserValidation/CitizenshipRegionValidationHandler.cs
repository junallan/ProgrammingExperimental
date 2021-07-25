using Chain_of_Responsibility_First_Look.Business.Exceptions;
using Chain_of_Responsibility_First_Look.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chain_of_Responsibility_First_Look.Business.Handlers.UserValidation
{
    public class CitizenshipRegionValidationHandler : Handler<User>
    {
        public override void Handle(User user)
        {
            if(user.CitizenshipRegion.TwoLetterISORegionName == "NO")
            {
                throw new UserValidationException("We currently do not support Norwegians");
            }

            base.Handle(user);
        }
    }
}
