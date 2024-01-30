namespace CapitalClue.Common.Models;

public class CsvFileBellRecordsAnalysisDto
{
    public List<BellRecordAnalysis> AnalysedRecords { get; set; } = new List<BellRecordAnalysis>();
    public bool IsFirstChunk { get; set; }
    public bool IsLastChunk { get; set; } = false;
    public string FileName { get; set; }

    public string UploadBy { get; set; }

    public int SuccessRowCount { get; set; }
    public int FailRowCount { get; set; }
    public List<string> HeaderErrorMessaed { get; set; } = new List<string>();
    public bool headerValid
    {
        get
        {
            return HeaderErrorMessaed.Count == 0;
        }
    }
}

public class BellRecordAnalysis
{
    public BellSourceDto BellRecord { get; set; }

    public bool IsSuccessful
    {
        get
        {
            return Errors == null || Errors.Count == 0;
        }
    }

    public int LineNumber { get; set; }
    public List<string> Errors { get; set; }

    public BellRecordAnalysis()
    {
        BellRecord = new BellSourceDto();
        Errors = new List<string>();
    }
}