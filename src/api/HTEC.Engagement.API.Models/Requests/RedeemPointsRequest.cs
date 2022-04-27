using System;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Engagement.API.Models.Requests
{
    /// <summary>
    /// Redeem Points Request
    /// </summary>
    public class RedeemPointsRequest
    {
        /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// How many points to redeem.
        /// </summary>
        [Required]
        public int Points { get; set; }
    }
}
