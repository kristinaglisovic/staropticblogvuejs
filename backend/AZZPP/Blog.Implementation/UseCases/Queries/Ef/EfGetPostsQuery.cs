using Blog.Application.UseCases.DTO.Post;
using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetPostsQuery :EfUseCase, IGetPostsQuery
    {
        public EfGetPostsQuery(BlogDbContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => "Search Posts";

        public string Description => "Search Posts using Entity Framework";

        public PagedResponse<PostDTO> Execute(PostPagedSearch search)
        {
            var query = Context.Posts.Include(x => x.User).ThenInclude(x => x.Role)
                                     .Include(x => x.Tags)
                                     .ThenInclude(x => x.Tag)
                                     .Include(x => x.Categories)
                                     .ThenInclude(x => x.Category)
                                     .Include(x => x.Comments)
                                     .Include(x => x.Images)
                                     .ThenInclude(x => x.Image)
                                     .Include(x => x.Likes).AsQueryable();


            if (search.HasComments.HasValue)
            {
                if (search.HasComments.Value)
                {
                    query = query.Where(p => p.Comments.Any());

                }
                else
                {
                    query = query.Where(p => !p.Comments.Any());
                }
            }


            if (search.IsActive.HasValue) {
                if (search.IsActive.Value)
                {
                    query = query.Where(p => p.IsActive);
                }
                else
                {
                    query = query.Where(p => !p.IsActive);
                }

            }


            if (search.HasLikes.HasValue)
            {
                if (search.HasLikes.Value)
                {
                    query = query.Where(p => p.Likes.Any());

                }
                else
                {
                    query = query.Where(p => !p.Likes.Any());
                }
            }

            var kw = search.Keyword;


            if (!string.IsNullOrEmpty(kw))
            {
                query = query.Where(x => x.User.Username.Contains(kw) ||
                                        x.User.LastName.Contains(kw) ||
                                        x.User.FirstName.Contains(kw) ||
                                        x.Tags.Any(x => x.Tag.Name.Contains(kw)) ||
                                        x.Categories.Any(x => x.Category.Name.Contains(kw)) ||
                                        x.Images.Any(x => x.Image.Path.Contains(kw)) ||
                                        x.Title.Contains(kw) || x.Description.Contains(kw));
            }


            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 4;
            }

            if(search.Page ==null || search.Page < 1)
            {
                search.Page = 1;
            }


            if (search.CategoryIds != null && search.CategoryIds.Any())
            {
                query = query.Where(x => x.Categories.Any(c => search.CategoryIds.Contains(c.CategoryId)));
            }

            if (search.TagsIds != null && search.TagsIds.Any())
            {
                query = query.Where(x => x.Tags.Any(c => search.TagsIds.Contains(c.TagId)));
            }


            if (search.SortOrder != null)
            {
                switch (search.SortOrder)
                {
                    case PostsOrder.LikesAsc:
                        query = query.OrderBy(x => x.Likes.Count);
                        break;
                    case PostsOrder.LikesDesc:
                        query = query.OrderByDescending(x => x.Likes.Count);
                        break;
                    case PostsOrder.CommentsAsc:
                        query = query.OrderBy(x => x.Comments.Count);
                        break;
                    case PostsOrder.CommentsDesc:
                        query = query.OrderByDescending(x => x.Comments.Count);
                        break;
                    default: query = query.OrderByDescending(x => x.CreatedAt);
                        break;
                }
            }


            var toSkip=(search.Page.Value-1)*search.PerPage.Value;

            var response = new PagedResponse<PostDTO>();
            response.TotalCount = query.Count();
            response.Data=query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new PostDTO
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Images = x.Images.Select(x => x.Image.Path),
                Author = $"{x.User.FirstName} {x.User.LastName} - {x.User.Username}",
                AuthorRole = x.User.Role.Name,
                CommentsCount = x.Comments.Where(y => y.IsActive).Count(),
                Comments = x.Comments.Where(y => y.IsActive).Select(x => new CommentDto
                {
                    Id = x.Id,
                    Comment = x.Text,
                    CommentedAt = x.CreatedAt,
                    User = x.User.Username
                }).ToList(),
                Tags = x.Tags.Select(x => x.Tag.Name),
                Categories = x.Categories.Select(x => x.Category.Name),
                Likes = x.Likes.Where(y=>y.IsActive).Count(),
                LikesInfo=x.Likes.Where(x=>x.IsActive).Select(x=>new LikeDto
                {
                    Id=x.Id,
                    Username=x.User.Username,
                    UserId=x.User.Id
                }).ToList(),
                CreatedAt = x.CreatedAt.ToString("yyyy-MM-dd hh:mm:ss"),
                IsActive=x.IsActive,
                ImagesCount=x.Images.Count(),
                UpdatedAt=x.UpdatedAt
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage=search.PerPage.Value;
            

            return response;

            
        }
    }
}
