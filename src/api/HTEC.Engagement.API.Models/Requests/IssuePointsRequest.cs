using System;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Engagement.API.Models.Requests
{
    /// <summary>
    /// Issue Points Request
    /// </summary>
    public class IssuePointsRequest
    {
        /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// How many points to issue
        /// </summary>
        [Required]
        public int Points { get; set; }
    }
}
