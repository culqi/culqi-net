using Culqi.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Culqi.Exceptions
{
    public class CulqiException : Exception
    {
        public CulqiException()
        {
        }

        public CulqiException(string message)
            : base(message)
        {
        }

        public CulqiException(string message, Exception err)
            : base(message, err)
        {
        }

        public CulqiException(HttpStatusCode httpStatusCode, CulqiError CulqiError, string message)
            : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.CulqiError = CulqiError;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public CulqiError CulqiError { get; set; }

        public CulqiResponse CulqiResponse { get; set; }
    }
}
