using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team11_SSIS_ADProject.SSIS.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTime createdDateTime { get; set; }
        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.createdDateTime = DateTime.Now;
        }
    }
}