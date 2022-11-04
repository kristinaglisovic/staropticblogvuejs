using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO.Post
{
    public class EditPostDTO
    {
        
            public int PostId { get; set; }
            public string Title { get; set; }

            public string Description { get; set; }
            public string Heading1 { get; set; }
            public string Heading2 { get; set; }
            public string Text1 { get; set; }
            public string Text2 { get; set; }


            public IEnumerable<int> CategoriesIds { get; set; }
            public IEnumerable<int> TagsIds { get; set; }
            public IEnumerable<int> ImagesIds { get; set; }

        
    }
}
