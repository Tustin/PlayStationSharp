using System;

namespace PSNSharp.Exceptions
{
    [Serializable]
    public class NpssoIdNotFoundException : Exception
    {
        public NpssoIdNotFoundException()
        {
        }

        public NpssoIdNotFoundException(string message) : base(message)
        {
        }

        public NpssoIdNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    [Serializable]
    public class XnpGrantCodeNotFoundException : Exception
    {
        public XnpGrantCodeNotFoundException()
        {
        }

        public XnpGrantCodeNotFoundException(string message) : base(message)
        {
        }

        public XnpGrantCodeNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }

    [Serializable]
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

    [Serializable]
    public class DualAuthSMSRequiredException : Exception
    {
        public DualAuthSMSRequiredException()
        {
        }

        public DualAuthSMSRequiredException(string message) : base(message)
        {
        }

        public DualAuthSMSRequiredException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}