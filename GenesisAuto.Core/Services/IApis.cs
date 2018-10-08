using System;
using System.Collections.Generic;
using System.Text;

namespace GenesisAuto.Core.Services
{
    public interface IApis
    {
        IGitHubApi GitHub { get; set; }
    }
}
