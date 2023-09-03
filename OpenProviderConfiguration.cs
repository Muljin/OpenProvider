using System;
namespace OpenProvider
{
    public record OpenProviderConfiguration
    {
        public string Username { get; init; } = null!;

        public string Password { get; init; } = null!;

    }
}

