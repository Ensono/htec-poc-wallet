using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Engagement.CQRS.Queries.GetPointsById
{
    public class Points
    {
        [Required]
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int Balance { get; set; }

        [Required]
        public bool? Enabled { get; set; }
    }
}
