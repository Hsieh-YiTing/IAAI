using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IAAI.Models.BaseEntityModel
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public DateTime UpdateTime { get; set; }
    }
}