using Chain_of_Responsibility_First_Look.Business.Exceptions;
using Chain_of_Responsibility_First_Look.Business.Handlers.UserValidation;
using Chain_of_Responsibility_First_Look.Business.Models;
using Chain_of_Responsibility_First_Look.Business.Validators;

namespace Chain_of_Responsibility_First_Look.Business
{
    public class UserProcessor
    {
        public bool Register(User user)
        {
            try
            {
                var handler = new SocialSecurityNumberValidatorHandler();

                handler.SetNext(new AgeValidationHandler())
                       .SetNext(new NameValidationHandler())
                       .SetNext(new CitizenshipRegionValidationHandler());

                handler.Handle(user);
            }
            catch (UserValidationException ex)
            {
                return false;
            }

            return true;


            //if (!socialSecurityNumberValidator.Validate(user.SocialSecurityNumber, user.CitizenshipRegion))
            //{
            //    return false;
            //}
            //else if (user.Age < 18)
            //{
            //    return false;
            //}
            //else if (user.Name.Length <= 1)
            //{
            //    return false;
            //}
            //else if (user.CitizenshipRegion.TwoLetterISORegionName == "NO")
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }
    }
}
