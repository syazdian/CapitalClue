namespace Bell.Reconciliation.Frontend.Desktop.Data;

public static class ProjectConfig
{
    public static string DatabasePath => $"Filename={Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\BellRec.db";
}