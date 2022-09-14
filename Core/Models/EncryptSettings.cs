using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models;

/// <summary>
/// <see cref="EncryptSettings"/> appSettings.json model
/// </summary>
public sealed class EncryptSettings
{
    public string PrivateKey { get; set; }
}
