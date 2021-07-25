using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GeekStream.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GeekStream.Core.Entities
{
    public class Subscription
    {
        public Subscription()
        {
            
        }

        [Key]
        public ApplicationUser ApplicationUser  { get; set; }

        [Key]
        public string PublishSource { get; set; }

    }
}