using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._4_DependencyInversion
{
    internal class DependencyInversionBadWay
    {
        public class MyFeatureThatUsesApi 
        {
            public void DoSomethingInteresting() 
            {
                var myApi = new MyFeatureThatUsesApi();
                myApi.DoSomethingInteresting();
            }
        }

        public class MyExternalUsefulApi 
        {
            public void DoSomethingInteresting() { }
            public void DoSomethingIDontReallyNeed() { }
        }
    }
}
