using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HTEC.Engagement.API.Models.Responses
{
    /// <summary>
    /// Response model used by GetById api endpoint
    /// </summary>
    public class Points
    {
        /// <example>d290f1ee-6c54-4b01-90e6-d701748f0851</example>
        [Required]
        public Guid? Id { get; set; }

        /// <example>Points name</example>
        [Required]
        public string Name { get; set; }

        /// <example>Points description</example>
        public string Description { get; set; }

        /// <summary>
        /// Represents the number of points. False if disabled
        /// </summary>
        [Required]
        public int Balance { get; set; }

        /// <summary>
        /// Represents the status of the points. False if disabled
        /// </summary>
        [Required]
        public bool? Enabled { get; set; }
    }
}
