namespace Reviews.Exception
{
    public class BadRequest
    {
        public int Code { get; set; } = 4000;
        public string Message { get; set; } = "";

        public BadRequest(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public class InvalidInput : BadRequest
    {
        public InvalidInput(int code, string message) : base(code, message)
        {
            Code = 4001;
            Message = "\nInvalid input! \n" +
                      "To logout and return back to main menu       : 1 \n" +
                      "To make a new choice                         : 2 ";
        }
    }
    
    public class InvalidPassword : BadRequest
    {
        public InvalidPassword(int code, string message) : base(code, message)
        {
            Code = 4002;
            Message = "";
        }
    }
}