using System;
using System.Text;

namespace SBP2;

public static class Extensions {
    public static string FormatExceptionMessage(this Exception ex) {
        StringBuilder sb = new();
        Exception? temp = ex;
        int indent = 0;

        while (temp != null) {
            if (indent > 0) {
                sb.Append($"{'-'.Repeat(indent)}> ");
            }

            sb.AppendLine(temp.Message);
            indent += 2;
            temp = temp.InnerException;
        }

        return sb.ToString();
    }

    private static string Repeat(this char c, int count) {
        return new string(c, count);
    }
}