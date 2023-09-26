using System;
using System.Collections.Generic;

namespace BookStoreApp.Models;
public class Project
{
    public int ProjectID { get; set; }
    public string ProjectName { get; set; }
    public string NumberOfModules { get; set; }
    public DateTime SubmissionDate { get; set; }
    public string FreelancerName { get; set; }
}
