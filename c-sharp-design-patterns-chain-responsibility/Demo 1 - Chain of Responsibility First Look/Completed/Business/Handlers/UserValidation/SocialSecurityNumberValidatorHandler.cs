using Chain_of_Responsibility_First_Look.Business.Exceptions;
using Chain_of_Responsibility_First_Look.Business.Models;
using Chain_of_Responsibility_First_Look.Business.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chain_of_Responsibility_First_Look.Business.Handlers.UserValidation
{
    public class SocialSecurityNumberValidatorHandler : Handler<User>
    {
        private SocialSecurityNumberValidator socialSecurityNumberValidator
            = new SocialSecurityNumberValidator();

        public override void Handle(User request)
        {
            if (!socialSecurityNumberValidator.Validate(request.SocialSecurityNumber,
                request.CitizenshipRegion))
            {
                throw new UserValidationException("Social security number could not be validated");
            }
            base.Handle(request);
        }
    }
}
