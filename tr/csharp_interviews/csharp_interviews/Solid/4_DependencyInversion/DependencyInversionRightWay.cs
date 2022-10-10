using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_interviews.Solid._4_DependencyInversion
{
    //higher level layer should only depend on lower level by using abstractions
    internal class DependencyInversionRightWay
    {
        public class MyFeatureThatUsesApi
        {
            private readonly IExternalUsefulApi _externalUsefulApi;

            public MyFeatureThatUsesApi(IExternalUsefulApi externalUsefulApi)
            {
                this._externalUsefulApi = externalUsefulApi;
            }

            public void DoSomethingInteresting()
            {
                _externalUsefulApi.DoSomethingInteresting();
            }
        }

        public interface IExternalUsefulApi 
        {
            public void DoSomethingInteresting();
        }

        public class MyExternalUsefulApi : IExternalUsefulApi
        {
            public void DoSomethingInteresting() { }
            public void DoSomethingIDontReallyNeed() { }
        }
    }
}
