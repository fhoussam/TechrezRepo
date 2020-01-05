using System;

namespace auth.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public ErrorViewModel()
        {
        }

        public ErrorViewModel(string error)
        {
            Error = new ErrorMessage { Error = error };
        }

        public ErrorMessage Error { get; set; }
    }

    public class ErrorMessage { 
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
        public string RequestId { get; set; }
    }
}