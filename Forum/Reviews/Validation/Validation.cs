using System.Linq;
using FluentValidation;
using Reviews.Models;

namespace Reviews.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull().WithMessage("Your first name cannot be empty!");
            RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("Your last name cannot be empty!");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Your e-mail cannot be empty!");
            RuleFor(x => x.Password).NotEmpty().NotNull().Length(8, 24)
                .WithMessage("Please enter a password 8 to 24 characters.");
            RuleFor(x => x.Email).Must(EndsWith)
                .WithMessage("Please give a valid domain name such as hotmail, google, outlook");
        }

        private static bool EndsWith(string email)
        {
            string[] mailEndings = {"@hotmail.com", "@gmail.com", "@outlook.com", "@yahoo.com"};
            return mailEndings.Any(email.EndsWith);
        }
    }
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().WithMessage("Title cannot be empty!");
            RuleFor(x => x.Content).NotEmpty().NotNull().WithMessage("Content cannot be empty!");
            RuleFor(x => x.Star).NotEmpty().NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(5).WithMessage("Invalid input for star value. Please enter an integer 1 to 5.");
            RuleFor(x => x.RejectReason).NotEmpty().WithMessage("Reject reason cannot be empty!");
        }
    }
}