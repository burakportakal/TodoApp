namespace TodoApp.Models.BusinessModels
{
    public class ResponseBase : ICommandResult
    {
        public ResultModel Result { get; set; }
    }

    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public ErrorModel Error { get; set; }

    }
    public class ErrorModel
    {
        public string ErrorText { get; set; }
        public string ErrorCode { get; set; }
        public string Exception { get; set; }
    }
}
