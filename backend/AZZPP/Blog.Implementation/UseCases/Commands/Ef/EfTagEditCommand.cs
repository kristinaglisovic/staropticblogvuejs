using Blog.Application.Exeptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO.Tag;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands.Ef
{
    public class EfTagEditCommand : EfUseCase, ITagEditCommand
    {
        private EditTagDTOValidator _validator;
        public EfTagEditCommand(BlogDbContext context, EditTagDTOValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 67;

        public string Name => "Edit tag";

        public string Description => "Ef edit tag";

        public void Execute(UpdateTagDTO request)
        {
            
            _validator.ValidateAndThrow(request);

            var tag = Context.Tags.Find(request.TagId);


            if (tag == null)
            {
                throw new EntityNotFoundException(nameof(Tag), tag.Id);
            }

            tag.Name = request.Name;

            tag.UpdatedAt = DateTime.UtcNow;


            Context.SaveChanges();
        }
    }
}
