using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace DemoDocumentFormatOpenXml.Runners
{
    internal class PieChartCloneRunner
    {
        public PieChartCloneRunner()
        {
        }
        public void Run()
        {
            // ...
            //File.Copy("source.docx", "target.docx", true);
            //using (var document = WordprocessingDocument.Open("target.docx", true))
            //{
            //    var mainPart = document.MainDocumentPart!;
            //    var chartPart = mainPart.ChartParts.First();
            //    // ocumentFormat.OpenXml.Packaging.ChartPart
            //    {
            //        var openXmlPart = chartPart;
            //        var rootElement = openXmlPart.RootElement;
            //        Console.WriteLine($"ChartPart:{mainPart.GetIdOfPart(chartPart)}");

            //        var schemeColor = chartPart.ChartSpace.Descendants<DocumentFormat.OpenXml.Drawing.SchemeColor>();

            //        Console.WriteLine($"ChartPart:{mainPart.GetIdOfPart(chartPart)}");
            //    }
            //    Console.WriteLine($"Parts:{chartPart.Parts.Count()}");
            //    //DocumentFormat.OpenXml.Packaging.EmbeddedPackagePart
            //    {
            //        var openXmlPart = chartPart.EmbeddedPackagePart!;
            //        var rootElement = openXmlPart.RootElement;
            //        Console.WriteLine($"EmbeddedPackagePart:{chartPart.GetIdOfPart(openXmlPart)}");
            //    }
            //    //DocumentFormat.OpenXml.Packaging.ChartColorStylePart
            //    {
            //        var openXmlPart = chartPart.ChartColorStyleParts!.First();
            //        var rootElement = openXmlPart.RootElement;
            //        Console.WriteLine($"ChartColorStylePart:{chartPart.GetIdOfPart(openXmlPart)}");
            //    }
            //    //DocumentFormat.OpenXml.Packaging.ChartStylePart
            //    {
            //        var openXmlPart = chartPart.ChartStyleParts!.First();
            //        var rootElement = openXmlPart.RootElement;
            //        Console.WriteLine($"ChartStylePart:{chartPart.GetIdOfPart(openXmlPart)}");
            //    }
            //    // Drawing
            //    //var runs = mainPart.Document.Descendants<Run>();
            //    //var drawing = runs.Select(s => s.Descendants<Drawing>()).First();
            //    mainPart.Document.Save();
            //}

            // ...
            //using (var sourceDocument = WordprocessingDocument.Open("source.docx", true))
            ////using (var targetDocument = WordprocessingDocument.Open("target.docx", true))
            //using (var targetDocument = WordprocessingDocument.Create("target.docx", WordprocessingDocumentType.Document))
            //{
            //    var sourceMainPart = sourceDocument.MainDocumentPart!;
            //    //var targetMainPart = targetDocument.MainDocumentPart!;
            //    var targetMainPart = targetDocument.AddMainDocumentPart();
            //    new Document(new Body()).Save(targetMainPart);
            //    var paras = sourceMainPart.Document.Descendants<Run>();
            //    var drawingElements = from run in paras
            //                          where run.Descendants<Drawing>().Count() != 0
            //                          select run.Descendants<Drawing>().First();
            //    var rId = "";
            //    sourceMainPart.ChartParts.AsParallel().ForAll(chartPart =>
            //    {
            //        Console.WriteLine("ChartParts");
            //        //targetMainPart.AddPart(chartPart, sourceMainPart.GetIdOfPart(chartPart));
            //        var part = targetMainPart.AddPart(chartPart);
            //        rId = targetMainPart.GetIdOfPart(part);
            //        Console.WriteLine("rId:" + rId);
            //    });
            //    drawingElements.AsParallel().ForAll(drawingElement =>
            //    {
            //        Console.WriteLine("drawingElements");
            //        var clone = (Drawing)drawingElement.Clone();
            //        var chartReference = clone.Descendants<ChartReference>().First();
            //        var attribute = chartReference.GetAttribute("id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            //        chartReference.RemoveAttribute(attribute.LocalName, attribute.NamespaceUri);
            //        chartReference.SetAttribute(new OpenXmlAttribute(attribute.Prefix, attribute.LocalName, attribute.NamespaceUri, rId));
            //        targetMainPart.Document!.Body!.Append(clone);
            //    });
            //    targetMainPart.Document.Save();
            //}

            // ...
            File.Copy("source.docx", "target.docx", true);
            using (var document = WordprocessingDocument.Open("target.docx", true))
            {
                var mainPart = document.MainDocumentPart!;
                var dataCount = 18;
                var sheetName = "Sheet";
                var chartPart = mainPart.ChartParts.First();
                Console.WriteLine($"ChartPart:{mainPart.GetIdOfPart(chartPart)}");
                Console.WriteLine($"Parts:{chartPart.Parts.Count()}");
                //DocumentFormat.OpenXml.Packaging.EmbeddedPackagePart
                {
                    var embeddedPackagePart = chartPart.EmbeddedPackagePart!;
                    var rootElement = embeddedPackagePart.RootElement;
                    Console.WriteLine($"EmbeddedPackagePart:{chartPart.GetIdOfPart(embeddedPackagePart)}");
                    if (chartPart.EmbeddedPackagePart!.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        using (var spreadsheetDocument = SpreadsheetDocument.Open(embeddedPackagePart.GetStream(), true))
                        {
                            var workbookPart = spreadsheetDocument.WorkbookPart!;
                            var sheets = spreadsheetDocument.WorkbookPart!.Workbook.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheets>().First();
                            sheets.RemoveAllChildren<DocumentFormat.OpenXml.Spreadsheet.Sheet>();
                            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                            var sheetData = new SheetData();
                            worksheetPart.Worksheet = new Worksheet(sheetData);
                            sheets.Append(new Sheet()
                            {
                                Id = workbookPart.GetIdOfPart(worksheetPart),
                                SheetId = 1,
                                Name = "Sheet"
                            });
                            {
                                var row = new DocumentFormat.OpenXml.Spreadsheet.Row();
                                row.Append(new DocumentFormat.OpenXml.Spreadsheet.Cell
                                {
                                    CellValue = new CellValue(""),
                                    DataType = new EnumValue<CellValues>(CellValues.String)
                                });
                                row.Append(new DocumentFormat.OpenXml.Spreadsheet.Cell
                                {
                                    CellValue = new CellValue("Target"),
                                    DataType = new EnumValue<CellValues>(CellValues.String),
                                });
                                sheetData.Append(row);
                            }
                            for (var i = 0; i < dataCount; ++i)
                            {
                                var row = new DocumentFormat.OpenXml.Spreadsheet.Row();
                                row.Append(new DocumentFormat.OpenXml.Spreadsheet.Cell
                                {
                                    CellValue = new CellValue($"[{i + 1}]"),
                                    DataType = new EnumValue<CellValues>(CellValues.String),
                                });
                                row.Append(new DocumentFormat.OpenXml.Spreadsheet.Cell
                                {
                                    CellValue = new CellValue($"{(i + 1) * 10}"),
                                    DataType = new EnumValue<CellValues>(CellValues.Number),
                                });
                                sheetData.Append(row);
                            }
                            Console.WriteLine();
                        }
                    }
                }
                //DocumentFormat.OpenXml.Packaging.ChartColorStylePart
                {
                    var openXmlPart = chartPart.ChartColorStyleParts!.First();
                    var rootElement = openXmlPart.RootElement;
                    Console.WriteLine($"ChartColorStylePart:{chartPart.GetIdOfPart(openXmlPart)}");
                }
                //DocumentFormat.OpenXml.Packaging.ChartStylePart
                {
                    var openXmlPart = chartPart.ChartStyleParts!.First();
                    var rootElement = openXmlPart.RootElement;
                    Console.WriteLine($"ChartStylePart:{chartPart.GetIdOfPart(openXmlPart)}");
                }
                // DocumentFormat.OpenXml.Drawing.Charts.ChartSpace
                {
                    Console.WriteLine($"ChartSpace");
                    // c:chartSpace {DocumentFormat.OpenXml.Drawing.Charts.ChartSpace}
                    var chartSpaceElement = chartPart.ChartSpace!;
                    // c:chart {DocumentFormat.OpenXml.Drawing.Charts.Chart}
                    var chartElement = chartSpaceElement
                        .GetFirstChild<DocumentFormat.OpenXml.Drawing.Charts.Chart>()!;
                    // c:plotArea {DocumentFormat.OpenXml.Drawing.Charts.PlotArea}
                    var plotAreaElement = chartElement
                        .GetFirstChild<DocumentFormat.OpenXml.Drawing.Charts.PlotArea>()!;
                    // c:pieChart {DocumentFormat.OpenXml.Drawing.Charts.PieChart}
                    var pieChartElement = plotAreaElement
                        .GetFirstChild<DocumentFormat.OpenXml.Drawing.Charts.PieChart>()!;
                    // c:ser {DocumentFormat.OpenXml.Drawing.Charts.PieChartSeries}
                    var pieChartSeriesElement = pieChartElement
                        .GetFirstChild<DocumentFormat.OpenXml.Drawing.Charts.PieChartSeries>()!;
                    // c:tx {DocumentFormat.OpenXml.Drawing.Charts.SeriesText}
                    var seriesTextElement = pieChartSeriesElement
                        .Descendants<DocumentFormat.OpenXml.Drawing.Charts.SeriesText>().First()!;
                    // c:strRef {DocumentFormat.OpenXml.Drawing.Charts.StringReference}
                    // c:f {ocumentFormat.OpenXml.Drawing.Charts.Formula}
                    // c:strCache {DocumentFormat.OpenXml.Drawing.Charts.StringCache}
                    // c:pt {DocumentFormat.OpenXml.Drawing.Charts.StringPoint}
                    // c:v {DocumentFormat.OpenXml.Drawing.Charts.NumericValue}
                    {
                        //var stringReferenceElement = seriesTextElement
                        //    .Descendants<DocumentFormat.OpenXml.Drawing.Charts.StringReference>().First();
                        //var stringCacheElement = stringReferenceElement
                        //    .Descendants<DocumentFormat.OpenXml.Drawing.Charts.StringCache>().First();
                        //var stringPointElement = stringCacheElement
                        //    .Descendants<DocumentFormat.OpenXml.Drawing.Charts.StringPoint>().First();
                        //var numericValueElement = stringPointElement
                        //    .Descendants<DocumentFormat.OpenXml.Drawing.Charts.NumericValue>().First();
                        //numericValueElement.Text = "Target";
                        seriesTextElement.Descendants<DocumentFormat.OpenXml.Drawing.Charts.NumericValue>().First().Text = "Target";
                    }
                    // c:dPt {DocumentFormat.OpenXml.Drawing.Charts.DataPoint}
                    var dataPointElements = pieChartSeriesElement
                        .Descendants<DocumentFormat.OpenXml.Drawing.Charts.DataPoint>();
                    // c:idx {DocumentFormat.OpenXml.Drawing.Charts.Index}
                    // c:spPr {DocumentFormat.OpenXml.Drawing.Charts.ChartShapeProperties}
                    // a:solidFill {DocumentFormat.OpenXml.Drawing.SolidFill}
                    // a:schemeClr {DocumentFormat.OpenXml.Drawing.SchemeColor}
                    // a:lumMod {DocumentFormat.OpenXml.Drawing.LuminanceModulation}
                    // a:lumOff {DocumentFormat.OpenXml.Drawing.LuminanceOffset}
                    {
                        var dataPointClone = (DocumentFormat.OpenXml.Drawing.Charts.DataPoint)dataPointElements.First().Clone();
                        OpenXmlElement anchor = seriesTextElement;
                        pieChartSeriesElement.RemoveAllChildren<DocumentFormat.OpenXml.Drawing.Charts.DataPoint>();
                        var lumMod = new[] {
                            "",
                            "60000",
                            "80000",
                            "80000",
                            "60000",
                            "50000",
                            "70000",
                        };
                        var lumOff = new[] {
                            "",
                            "",
                            "20000",
                            "",
                            "40000",
                            "",
                            "30000",
                        };
                        for (var i = 0; i < dataCount; ++i)
                        {
                            var dataPoint = (DocumentFormat.OpenXml.Drawing.Charts.DataPoint)dataPointClone.Clone();
                            var index = dataPoint.Descendants<DocumentFormat.OpenXml.Drawing.Charts.Index>().First();
                            index.RemoveAttribute("val", "");
                            index.SetAttribute(new OpenXmlAttribute("", "val", "", $"{i}"));
                            var schemeColor = dataPoint.Descendants<DocumentFormat.OpenXml.Drawing.SchemeColor>().First();
                            schemeColor.RemoveAttribute("val", "");
                            schemeColor.SetAttribute(new OpenXmlAttribute("", "val", "", $"accent{(i % 6) + 1}"));
                            var lumIndex = i / 6;
                            if (!string.IsNullOrEmpty(lumMod[lumIndex]))
                            {
                                var luminanceModulation = new DocumentFormat.OpenXml.Drawing.LuminanceModulation();
                                luminanceModulation.SetAttribute(new OpenXmlAttribute("", "val", "", $"{lumMod[lumIndex]}"));
                                schemeColor.Append(luminanceModulation);
                            }
                            if (!string.IsNullOrEmpty(lumOff[lumIndex]))
                            {
                                var luminanceOffset = new DocumentFormat.OpenXml.Drawing.LuminanceOffset();
                                luminanceOffset.SetAttribute(new OpenXmlAttribute("", "val", "", $"{lumOff[lumIndex]}"));
                                schemeColor.Append(luminanceOffset);
                            }
                            //Console.WriteLine(schemeColor.OuterXml);
                            anchor = anchor.InsertAfterSelf(dataPoint);
                        }
                    }
                    // c:cat {DocumentFormat.OpenXml.Drawing.Charts.CategoryAxisData}
                    var categoryAxisDataElement = pieChartElement
                        .Descendants<DocumentFormat.OpenXml.Drawing.Charts.CategoryAxisData>()!.First();
                    // c:strRef {DocumentFormat.OpenXml.Drawing.Charts.StringReference}
                    // c:f {DocumentFormat.OpenXml.Drawing.Charts.Formula}
                    // c:strCache {DocumentFormat.OpenXml.Drawing.Charts.StringCache}
                    // c:ptCount {DocumentFormat.OpenXml.Drawing.Charts.PointCount}
                    // c:pt {DocumentFormat.OpenXml.Drawing.Charts.StringPoint}
                    // c:v {DocumentFormat.OpenXml.Drawing.Charts.NumericValue}
                    {
                        categoryAxisDataElement.Descendants<DocumentFormat.OpenXml.Drawing.Charts.Formula>()!.First().Text = $"{sheetName}!$A$2:$A${dataCount + 1}";
                        var pointCount = categoryAxisDataElement.Descendants<DocumentFormat.OpenXml.Drawing.Charts.PointCount>().First();
                        pointCount.RemoveAttribute("val", "");
                        pointCount.SetAttribute(new DocumentFormat.OpenXml.OpenXmlAttribute("", "val", "", $"{dataCount}"));
                        OpenXmlElement anchor = pointCount;
                        categoryAxisDataElement
                            .Descendants<DocumentFormat.OpenXml.Drawing.Charts.StringCache>().First()
                            .RemoveAllChildren<DocumentFormat.OpenXml.Drawing.Charts.StringPoint>();
                        for (var i = 0; i < dataCount; ++i)
                        {
                            var stringPoint = new DocumentFormat.OpenXml.Drawing.Charts.StringPoint();
                            stringPoint.SetAttribute(new DocumentFormat.OpenXml.OpenXmlAttribute("", "idx", "", $"{i}"));
                            var numericValue = new DocumentFormat.OpenXml.Drawing.Charts.NumericValue($"[{i + 1}]");
                            stringPoint.Append(numericValue);
                            anchor = anchor.InsertAfterSelf(stringPoint);
                        }
                    }
                    // c:val {DocumentFormat.OpenXml.Drawing.Charts.Values}
                    var valuesElement = pieChartElement
                        .Descendants<DocumentFormat.OpenXml.Drawing.Charts.Values>().First();
                    // c:numRef {DocumentFormat.OpenXml.Drawing.Charts.NumberReference}
                    // c:f {DocumentFormat.OpenXml.Drawing.Charts.Formula}
                    // c:numCache {DocumentFormat.OpenXml.Drawing.Charts.NumberingCache}
                    // c:formatCode {DocumentFormat.OpenXml.Drawing.Charts.FormatCode}
                    // c:ptCount {DocumentFormat.OpenXml.Drawing.Charts.PointCount}
                    // c:pt {DocumentFormat.OpenXml.Drawing.Charts.NumericPoint}
                    // c:v {DocumentFormat.OpenXml.Drawing.Charts.NumericValue}
                    {
                        valuesElement.Descendants<DocumentFormat.OpenXml.Drawing.Charts.Formula>()!.First().Text = $"{sheetName}!$B$2:$B${dataCount + 1}";
                        var pointCount = valuesElement.Descendants<DocumentFormat.OpenXml.Drawing.Charts.PointCount>().First();
                        pointCount.RemoveAttribute("val", "");
                        pointCount.SetAttribute(new DocumentFormat.OpenXml.OpenXmlAttribute("", "val", "", $"{dataCount}"));
                        OpenXmlElement anchor = pointCount;
                        valuesElement
                            .Descendants<DocumentFormat.OpenXml.Drawing.Charts.NumberingCache>().First()
                            .RemoveAllChildren<DocumentFormat.OpenXml.Drawing.Charts.NumericPoint>();
                        for (var i = 0; i < dataCount; ++i)
                        {
                            var numericPoint = new DocumentFormat.OpenXml.Drawing.Charts.NumericPoint();
                            numericPoint.SetAttribute(new DocumentFormat.OpenXml.OpenXmlAttribute("", "idx", "", $"{i}"));
                            var numericValue = new DocumentFormat.OpenXml.Drawing.Charts.NumericValue($"{(i + 1) * 10}");
                            numericPoint.Append(numericValue);
                            anchor = anchor.InsertAfterSelf(numericPoint);
                        }
                    }
                }
                // Drawing
                //var runs = mainPart.Document.Descendants<Run>();
                //var drawing = runs.Select(s => s.Descendants<Drawing>()).First();
                mainPart.Document.Save();
            }
        }
    }
}
