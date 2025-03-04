using Core.Model;
using RedditCloneMiniProjectAPI.Context;

namespace RedditCloneMiniProjectAPI.Repos
{
    public class PostRepo
    {
        PostContext db = new PostContext();
        public Post createPost(NewPost newPost) {


            return new Post();
        }
    }
}
