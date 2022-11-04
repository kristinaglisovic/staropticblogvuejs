﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Like:Entity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
