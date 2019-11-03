using System;
using System.Collections.Generic;

namespace WordSequenceFinder.Core.FindSequence
{
    public class HandlerResult
    {
        public bool IsSuccessful { get; set; } = false;
        public IEnumerable<string> ErrorList { get; set; } = new List<string>();
        public IEnumerable<Exception> Exceptions { get; set; } = new List<Exception>();
        public object Result { get; set; } = null;
    }
}