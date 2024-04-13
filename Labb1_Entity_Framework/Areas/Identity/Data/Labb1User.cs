using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb1_Entity_Framework.Models;
using Microsoft.AspNetCore.Identity;

namespace Labb1_Entity_Framework.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Labb1User class
public class Labb1User : IdentityUser
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public ICollection<VacationRequest> Vacations { get; set; } = new List<VacationRequest>();
}

