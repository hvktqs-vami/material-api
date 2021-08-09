using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mta.Vami.WebApi
{
    public abstract class BaseEntity
    {
        [Column]
        public DateTime CreatedTime { get; set; }

        [Column]
        public string CreatedUser { get; set; }

        [Column]
        public DateTime UpdatedTime { get; set; }

        [Column]
        public string UpdatedUser { get; set; }
    }

    public abstract class BaseEntity<TPrimaryKey>:BaseEntity
    {
        [PrimaryKey]
        public virtual TPrimaryKey Id { get; set; }
    
    }

    public abstract class Int32BaseEntity : BaseEntity<int>
    {
        [Identity]
        public override int Id { get => base.Id; set => base.Id = value; }
    }

    public abstract class Int64BaseEntity : BaseEntity<long>
    {
        [Identity]
        public override long Id { get => base.Id; set => base.Id = value; }
    }
}
