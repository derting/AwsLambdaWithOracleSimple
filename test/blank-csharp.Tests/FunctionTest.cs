using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using Amazon.XRay.Recorder.Core.Exceptions;
using Amazon.XRay.Recorder.Core.Sampling;
using Amazon.XRay.Recorder.Core.Internal.Context;
using Amazon.XRay.Recorder.Core.Internal.Utils;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.TestUtilities;

using blankCsharp;

namespace blankCsharp.Tests
{
    public class FunctionTest 
    {
        [Fact]
        public void TestFunction()
        {
            Assert.Equal("HELLO WORLD", "HELLO WORLD");
        }
    }
}
