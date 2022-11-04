using Blog.Application.UseCases.DTO.Tag;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Implementation.Validators
{
    public class EditTagDTOValidator:AbstractValidator<UpdateTagDTO>
    {


        private BlogDbContext _context;

        public EditTagDTOValidator(BlogDbContext context)
        {
            _context = context;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").MinimumLength(3).WithMessage("Tag name must have at least 3 characters.")
                                .Must(TagNotInUse).WithMessage("Tag sa nazivom '{PropertyValue}' već postoji."); ;
        }

        private bool TagNotInUse(string name)
        {
            return !_context.Tags.Any(x => x.Name == name);
        }
    }
}
