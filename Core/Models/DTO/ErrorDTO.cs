using Core.Models.Enums;

namespace Core.Models.DTO
{
    public class ErrorDTO
    {
        public ErrorCode ErrorCode { get; set; }
        public string ErrorDescription => GetDescription(ErrorCode);

        public string GetDescription(ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ErrorCode.UnknownError:
                    return "Unknown Error";
                case ErrorCode.EntityNotfound:
                    return "Entity not found";
                case ErrorCode.MatchNotFound:
                    return "Match not found";
                default:
                    return "Unknown error.";
            }
        }
    }
}