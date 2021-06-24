using System.IO;
using GrapeCity.Documents.Excel;
using diodocs.Models;
using System.Collections.Generic;
using diodocs.Factory;
using System.Linq;
using GrapeCity.Documents.Excel.Drawing;

namespace diodocs.Services
{
    public interface IConverter
    {
        byte[] ConvertExcelToPDF(ConvertRequest[] req);
    }

    public class Converter : IConverter
    {
        private readonly IPrintManager printManager;
        private readonly IWorkbookFactory workbookFactory;
        private readonly NLog.Logger logger;
        public Converter(IWorkbookFactory wf, IPrintManager pm)
        {
            this.printManager = pm;
            this.workbookFactory = wf;
            this.logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public byte[] ConvertExcelToPDF(ConvertRequest[] req)
        {
            byte[] outputData;

            List<IWorkbook> workbooks = new List<IWorkbook>();

            for (var i = 0; i < req.Length; i++)
            {
                using (MemoryStream mem = new MemoryStream(req[i].template))
                {
                    using (MemoryStream backgroundImage = new MemoryStream(req[i].backgroundImage))
                    {
                        IWorkbook workbook = workbookFactory.newInstance();
                        workbook.Open(mem, OpenFileFormat.Xlsx);
                        IPageSetup pageSetup = workbook.Worksheets[0].PageSetup;
                        IRange range = workbook.Worksheets[0].Range[pageSetup.PrintArea];
                        logger.Info("range.Width:{0} range.Height:{1}", range.Width, range.Height);
                        workbook.Worksheets[0].BackgroundPictures.AddPicture(backgroundImage, ImageType.PNG, 0, 0, range.Width, range.Height);
                        workbooks.Add(workbook);
                    }
                }
            }
            using (MemoryStream memOut = new MemoryStream())
            {
                printManager.SavePDF(memOut, workbooks.OfType<Workbook>().ToList());
                outputData = memOut.GetBuffer();
            }
            return outputData;
        }
    }
}