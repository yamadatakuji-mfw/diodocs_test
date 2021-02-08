using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GrapeCity.Documents.Excel;

namespace diodocs.Services
{
    public interface IPrintManager
    {
        public void SavePDF(Stream stream, List<Workbook> workbooks);
    }

    public class PrintManagerWrapper: IPrintManager
    {
        private readonly NLog.Logger logger;

        public PrintManagerWrapper()
        {
            this.logger = NLog.LogManager.GetCurrentClassLogger();
        }
        public void SavePDF(Stream stream, List<Workbook> workbooks)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            PrintManager printManager = new PrintManager();
            printManager.SavePDF(stream, workbooks);
            stopwatch.Start();
            logger.WithProperty("duration", stopwatch.ElapsedMilliseconds.ToString()).
                Debug("finish to save PDF.");
        }
    } 
}
