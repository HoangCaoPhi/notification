﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Shared;
public sealed class JwtOptionsSetup(IConfiguration configuration) : 
    IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration = configuration;

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(JwtOptions.Section).Bind(options);
    }
}
