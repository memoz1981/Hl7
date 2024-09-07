namespace Hl7.App.Utilities;

public interface IFileLogger
{
    public Task Log(Exception ex); 
}
