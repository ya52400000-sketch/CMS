namespace CMS.BLL;

public class CommonResponse
{
    public string Message { get; set; } = string.Empty;
    public bool IsSucceded { get; set; }
    public object AdditionalInfo { get; set; } = new();
    public List<string> Errors { get; set; }

    public CommonResponse(string msg, bool isSucceded, List<string> errors = null!, object additionalInfo = null!)
    {
        Message = msg;
        IsSucceded = isSucceded;
        Errors = errors;
        AdditionalInfo = additionalInfo;
    }
}
