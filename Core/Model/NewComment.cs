using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public record NewComment
    {
        public int userId { get; set; } = -1;
        public int postId { get; set; }
        public string comment { get; set; } = string.Empty;
    }
}
