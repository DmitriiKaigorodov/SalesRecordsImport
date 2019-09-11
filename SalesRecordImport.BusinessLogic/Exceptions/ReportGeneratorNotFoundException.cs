using System;

namespace SalesRecordImport.BusinessLogic.Exceptions
{
    public class ReportGeneratorNotFoundException : Exception
    {
        public Type RequestType { get; }
        public Type ResultType { get; }

        public ReportGeneratorNotFoundException(Type requestType, Type resultType)
            : base($"Report generator for request {requestType.Name} and result {resultType.Name} was not found.")
        {
            RequestType = requestType;
            ResultType = resultType;
        }
    }
}
