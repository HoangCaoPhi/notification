﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Web.Server.Identity;

public class IdentityContext(DbContextOptions<IdentityContext> options) : 
             IdentityDbContext<IdentityUser>(options)
{
 
}
