using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Exeptions
{
    public class EditValidationException:Exception
    {
        public EditValidationException(string message) : base(message)
        {

        }
    }
}
