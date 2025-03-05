using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public record NewPost
    {
        public int userId { get; set; }
        public string title { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
    }
}
