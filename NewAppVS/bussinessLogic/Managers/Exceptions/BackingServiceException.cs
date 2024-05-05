namespace UPB.BusinessLogic.Managers.Exceptions
{
    public class BackingServiceException : Exception
    {
        public BackingServiceException(string ex):base(ex) { }

        public string GetMessageForLogs(string methodName)
        {
            return methodName + " method error: " + Message;
        }
    }
}