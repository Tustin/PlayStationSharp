using System;

namespace PSN.Exceptions
{
    public class NPSSOIdNotFoundException : Exception
    {
        public NPSSOIdNotFoundException()
        {
        }

        public NPSSOIdNotFoundException(string message) : base(message)
        {
        }

        public NPSSOIdNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class XNPGrantCodeNotFoundException : Exception
    {
        public XNPGrantCodeNotFoundException()
        {
        }

        public XNPGrantCodeNotFoundException(string message) : base(message)
        {
        }

        public XNPGrantCodeNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
    public class OAuthTokenNotFoundException : Exception
    {
        public OAuthTokenNotFoundException()
        {
        }

        public OAuthTokenNotFoundException(string message) : base(message)
        {
        }

        public OAuthTokenNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
