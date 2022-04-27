using System;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Engagement.API.Models.Requests
{
    /// <summary>
    /// Request model used by CreatePoints api endpoint
    /// </summary>
    public class CreatePointsRequest
    {
        /// <example>Name of points created</example>
        [Required]
        public string Name { get; set; }

        /// <example>Description of points created</example>
        [Required]
        public string Description { get; set; }

        /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
        [Required]
        public Guid TenantId { get; set; }

        /// <summary>
        /// Represents the status of the points. False if disabled
        /// </summary>
        [Required]
        public bool Enabled { get; set; }

        /// <summary>
        /// Represents the starting balance of the points.
        /// </summary>
        [Required]
        public int Balance {  get; set; }
    }
}
