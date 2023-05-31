using System;

namespace DemoDocumentFormatOpenXml.Utilities
{
    internal class XlsxUtility
    {
        public static string GetCellReference(uint cellIndex, uint rowIndex)
        {
            string cellText;
            if (cellIndex <= 26)
            {
                cellText = Convert.ToChar(64 + cellIndex).ToString();
            }
            else
            {
                cellText = Convert.ToChar(64 + (cellIndex % 26)).ToString();
                cellText += Convert.ToChar(64 + (cellIndex / 26)).ToString();
            }
            return $"{cellText}{rowIndex}";
        }
    }
}
