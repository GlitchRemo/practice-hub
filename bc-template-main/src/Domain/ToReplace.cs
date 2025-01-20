using System.Collections.Generic;
using NGrid.Customer.Framework.Templates.Abstraction;

namespace NGrid.Customer.ToReplace.Domain;

public class ToReplace : IBaseObject
{
    public string? Description { get; set; }
    public int Key { get; set; }
    public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
}