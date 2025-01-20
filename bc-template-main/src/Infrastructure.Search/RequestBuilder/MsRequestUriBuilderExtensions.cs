using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Azure.Core;

namespace NGrid.Customer.ToReplace.Infrastructure.Search.RequestBuilder;

[ExcludeFromCodeCoverage]
internal static class MsRequestUriBuilderExtensions
{
    public static void AppendQueryDelimited<T>(this RequestUriBuilder builder, string name, IEnumerable<T> value, string delimiter, bool escape = true)
    {
        var stringValues = value.Select(v => ConvertToString(v));
        builder.AppendQuery(name, string.Join(delimiter, stringValues), escape);
    }

    private static string ConvertToString(object value, string format = null)
        => value switch
        {
            null => "null",
            string s => s,
            int or float or double or long or decimal => ((IFormattable)value).ToString("G", CultureInfo.InvariantCulture),
            IEnumerable<string> s => string.Join(",", s),
            _ => value.ToString()!
        };
}
