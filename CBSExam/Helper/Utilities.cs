using System;

namespace CBSExam.Helper
{
    public static class Utilities
    {
        [Flags]
        public enum ErrorCodes
        {
            LoadingPageError = 1,
            SubmitMessageError = 2
        }
    }
}
