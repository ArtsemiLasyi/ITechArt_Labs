﻿using System;
using System.Collections.Generic;

namespace DataAccess.Entities
{
    public partial class UserRoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
