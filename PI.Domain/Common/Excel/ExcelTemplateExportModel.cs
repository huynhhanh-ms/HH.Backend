namespace PI.Domain.Common.Excel
{
    public class ExcelTemplateExportModel
    {
        public List<string> Headers { get; set; }
        public List<string> ValueHeaders { get; set; }
        public string FileName { get; set; }
        public string SheetName { get; set; }
        public List<string> StringFormatColumns { get; set; }
        public List<string> DateFormatColumns { get; set; }
        public List<string> DateTimeFormatColumns { get; set; }
        public List<ExcelTemplateAdditionalSheetInfo> AdditionalSheetInfos { get; set; }
    }
}
