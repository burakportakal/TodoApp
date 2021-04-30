namespace TodoApp.Models.BusinessModels
{
    public class UserLoginRequest: RequestBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
