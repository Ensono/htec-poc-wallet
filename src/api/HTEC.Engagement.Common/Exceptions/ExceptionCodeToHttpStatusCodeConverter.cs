using System.Net;

namespace HTEC.Engagement.Common.Exceptions
{
    public static class ExceptionCodeToHttpStatusCodeConverter
    {
        internal static HttpStatusCode GetHttpStatusCode(int exceptionCode)
        {
            switch ((ExceptionCode)exceptionCode)
            {
                case Exceptions.ExceptionCode.UnauthorizedOperation:
                    return HttpStatusCode.Unauthorized;
                case Exceptions.ExceptionCode.ForbiddenOperation:
                    return HttpStatusCode.Forbidden;
                case Exceptions.ExceptionCode.BadRequest:
                    return HttpStatusCode.BadRequest;
                case Exceptions.ExceptionCode.NotFound:
                    return HttpStatusCode.NotFound;
                case Exceptions.ExceptionCode.Conflict:
                    return HttpStatusCode.Conflict;
                case Exceptions.ExceptionCode.FeatureDisabled:
                    return HttpStatusCode.NotFound;
                case Exceptions.ExceptionCode.CircuitBreakerEnabled:
                    return HttpStatusCode.ServiceUnavailable;

                //Business related
                case Exceptions.ExceptionCode.PointsAlreadyExists:
                case Exceptions.ExceptionCode.PointsDoesNotExist:
                default:
                    return HttpStatusCode.BadRequest;
            }
        }
    }
}
