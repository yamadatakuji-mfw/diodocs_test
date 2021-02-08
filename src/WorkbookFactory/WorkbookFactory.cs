using GrapeCity.Documents.Excel;

namespace diodocs.Factory
{
    public interface IWorkbookFactory
    {
        IWorkbook newInstance();
    }

    public class WorkbookFactory : IWorkbookFactory
    {
        public IWorkbook newInstance()
        {
            return new Workbook();
        }
    }
}