﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
  public class JobRank
  {
    [Key]
    public string Uuid { get; set; }
    public string Title { get; set; }
    public int Salary { get; set; }
    
    public Job Job { get; set; }
    [ForeignKey("Job")]
    public string JobId { get; set; }

    public JobRank()
    {
      
    }

  }
}