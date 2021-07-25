using Chain_of_Responsibility_First_Look.Business.Exceptions;
using Chain_of_Responsibility_First_Look.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chain_of_Responsibility_First_Look.Business.Handlers.UserValidation
{
    public class AgeValidationHandler : Handler<User>
    {
        public override void Handle(User user)
        {
            if(user.Age < 18)
            {
                throw new UserValidationException("You have to be 18 years or older");
            }

            base.Handle(user);
        }
    }
}
