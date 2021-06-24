namespace Reviews.Exception
{
    public class BadRequest : System.Exception
    {
        public int Code { get; set; } = 4000;
        public string Message { get; set; } = "";

        protected BadRequest(int code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public class InvalidInput : BadRequest
    {
        public InvalidInput() : base(4001, "\nInvalid input! \n" +
                                                                   "To make a new choice                         : 1 \n" +
                                                                   "To logout and return back to main menu       : (any) ")
        {
        }
    }

    public class InvalidMenu : BadRequest
    {
        public InvalidMenu() : base(4002, "\nInvalid input! \n" +
                                          "To make a new choice                         : 1 \n" +
                                          "To exit                                      : (any) ")
        {
        }
    }

    public class InvalidPassword : BadRequest
    {
        public InvalidPassword() : base(4003, "\nWrong Password! ")
        {
        }
    }
    
    public class InvalidLogin : BadRequest
    {
        public InvalidLogin() : base(4004, "\nInvalid login attempt! \nReturning to main menu. ")
        {
        }
    }
    
    public class InvalidAdminInput : BadRequest
    {
        public InvalidAdminInput() : base(4005, "\nInvalid input! \n" +
                                           "To make a new choice                         : 1 \n" +
                                           "To return back to admin menu                 : (any) ")
        {
        }
    }
    
    public class InvalidUserInput : BadRequest
    {
        public InvalidUserInput() : base(4006, "\nInvalid input! \n" +
                                               "To make a new choice                         : 1 \n" +
                                               "To return back to user menu                  : (any) ")
        {
        }
    }
}