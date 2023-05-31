using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace DemoItext7
{
    internal class Runner
    {
        public Runner()
        { 
        }
        public Task Run()
        {
            using (var pdfWriter = new PdfWriter($"{Guid.NewGuid()}.pdf"))
            {
                var pdfDocument = new PdfDocument(pdfWriter);
                var document = new Document(pdfDocument, PageSize.A4);
                //var pdfFont = PdfFontFactory.CreateFont("D:\\Fonts\\msjh.ttc");
                //var fontBinary = PdfEncodings.ConvertToBytes("", PdfEncodings.UTF8);
                var pdfFont = PdfFontFactory.CreateFont("c:/Windows/Fonts/kaiu.ttf", 
                    PdfEncodings.IDENTITY_H,
                    PdfFontFactory.EmbeddingStrategy.FORCE_EMBEDDED);
                {
                    var bodyTable = default(Table);
                    //var table = new Table(UnitValue.CreatePercentArray(8)).UseAllAvailableWidth();
                    for (var i = 0; i < 1000; ++i)
                    {
                        //table.AddHeaderCell(new Paragraph("交易日期").SetFont(pdfFont).SetFontSize(10));
                        //table.AddCell(new Paragraph(datas[i].TRADE_DATE).SetFont(pdfFont).SetFontSize(10));
                        if (i == 0 || i % 20 == 0)
                        {
                            if (bodyTable != null)
                            {
                                // Title
                                {
                                    var titleParagraph = new Paragraph(new Text("Title"))
                                        .SetFont(pdfFont)
                                        .SetFontSize(20)
                                        .SetTextAlignment(TextAlignment.CENTER);
                                    document.Add(titleParagraph);
                                }
                                {
                                    var headerTable = new Table(UnitValue.CreatePercentArray(2))
                                        .UseAllAvailableWidth()
                                        .SetFont(pdfFont);
                                    {
                                        var cell = new Cell().SetTextAlignment(TextAlignment.RIGHT)
                                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                            .SetBorder(Border.NO_BORDER);
                                        cell.Add(new Paragraph(new Text("Date"))
                                            .SetFontSize(20));
                                        headerTable.AddCell(cell);
                                    }
                                    {
                                        var cell = new Cell().SetTextAlignment(TextAlignment.RIGHT)
                                            .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                            .SetBorder(Border.NO_BORDER);
                                        cell.Add(new Paragraph(new Text("First"))
                                            .SetFontSize(15));
                                        cell.Add(new Paragraph(new Text("Second"))
                                            .SetFontSize(15));
                                        headerTable.AddCell(cell);
                                    }
                                    document.Add(headerTable);
                                }
                                // Body
                                document.Add(bodyTable);
                                // Footer
                                document.Add(new Paragraph(""));
                                {
                                    var footerTable = new Table(UnitValue.CreatePercentArray(2))
                                        .UseAllAvailableWidth()
                                        .SetFont(pdfFont);
                                    {
                                        var cell = new Cell().SetTextAlignment(TextAlignment.LEFT)
                                            .SetBorder(Border.NO_BORDER);
                                        cell.Add(new Paragraph(new Text("Sub：")));
                                        footerTable.AddCell(cell);
                                    }
                                    {
                                        var cell = new Cell().SetTextAlignment(TextAlignment.LEFT)
                                            .SetBorder(Border.NO_BORDER);
                                        cell.Add(new Paragraph(new Text("Total：")));
                                        footerTable.AddCell(cell);
                                    }
                                    document.Add(footerTable);
                                }
                                document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                            }

                            bodyTable = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
                            bodyTable = bodyTable.SetFont(pdfFont).SetFontSize(10);
                            bodyTable.AddHeaderCell("A");
                            bodyTable.AddHeaderCell("B");
                            bodyTable.AddHeaderCell("C");
                            bodyTable.AddHeaderCell("D");
                        }
                        bodyTable!.AddCell($"A[{i + 1}]");
                        bodyTable!.AddCell($"B[{i + 1}]");
                        bodyTable!.AddCell($"C[{i + 1}]");
                        bodyTable!.AddCell($"D[{i + 1}]");
                    }

                    if (bodyTable != null)
                    {
                        // Title
                        {
                            var titleParagraph = new Paragraph(new Text("Title"))
                                .SetFont(pdfFont)
                                .SetFontSize(20)
                                .SetTextAlignment(TextAlignment.CENTER);
                            document.Add(titleParagraph);
                        }
                        {
                            var headerTable = new Table(UnitValue.CreatePercentArray(2))
                                .UseAllAvailableWidth()
                                .SetFont(pdfFont);
                            {
                                var cell = new Cell().SetTextAlignment(TextAlignment.RIGHT)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                    .SetBorder(Border.NO_BORDER);
                                cell.Add(new Paragraph(new Text("Date"))
                                    .SetFontSize(20));
                                headerTable.AddCell(cell);
                            }
                            {
                                var cell = new Cell().SetTextAlignment(TextAlignment.RIGHT)
                                     .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                     .SetBorder(Border.NO_BORDER);
                                cell.Add(new Paragraph(new Text("First"))
                                    .SetFontSize(15));
                                cell.Add(new Paragraph(new Text("Second"))
                                    .SetFontSize(15));
                                headerTable.AddCell(cell);
                            }
                            document.Add(headerTable);
                        }
                        // Body
                        document.Add(bodyTable);
                        // Footer
                        document.Add(new Paragraph(""));
                        {
                            var footerTable = new Table(UnitValue.CreatePercentArray(2))
                                .UseAllAvailableWidth()
                                .SetFont(pdfFont);
                            {
                                var cell = new Cell().SetTextAlignment(TextAlignment.LEFT)
                                    .SetBorder(Border.NO_BORDER);
                                cell.Add(new Paragraph(new Text("Sub：")));
                                footerTable.AddCell(cell);
                            }
                            {
                                var cell = new Cell().SetTextAlignment(TextAlignment.LEFT)
                                    .SetBorder(Border.NO_BORDER);
                                cell.Add(new Paragraph(new Text("Total：")));
                                footerTable.AddCell(cell);
                            }
                            document.Add(footerTable);
                        }
                        //document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
                    }
                }
                document.Close();
                pdfDocument.Close();
            }
            return Task.CompletedTask;
        }
    }
}
