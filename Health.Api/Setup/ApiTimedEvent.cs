using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web;

namespace Health.Setup
{
    internal class ApiTimedEvent : IDisposable
    {
        private readonly string _event;
        private readonly string _sourceFilePath;
        private readonly int _sourceLineNumber;
        private readonly object[] _memberParameters;
        private readonly Stopwatch _watch = new Stopwatch();
        private string _ipAddress;

        protected internal string IpAddress
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ipAddress) && HttpContext.Current != null)
                    _ipAddress = HttpContext.Current.Request.UserHostAddress;

                return _ipAddress;
            }
            set { _ipAddress = value; }
        }

        public ApiTimedEvent([CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0,
            params object[] memberParameters)
        {
            _event = memberName;
            _sourceFilePath = sourceFilePath;
            _sourceLineNumber = sourceLineNumber;
            _memberParameters = memberParameters;
            _watch.Start();
        }

        public ApiTimedEvent(bool withParams, string memberName,
            string sourceFilePath,
            int sourceLineNumber,
            params object[] memberParameters)
        {
            _event = memberName;
            _sourceFilePath = sourceFilePath;
            _sourceLineNumber = sourceLineNumber;
            _memberParameters = memberParameters;
            _watch.Start();
        }

        #region IDisposable Members

        public void Dispose()
        {
            _watch.Stop();
        }

        #endregion
    }
}