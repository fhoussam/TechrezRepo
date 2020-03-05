using System;
using System.Collections.Generic;
using System.Text;

namespace jh
{
    public enum DateParserType
    {
        DateParserType0 = 0,
        DateParserType1 = 1,
        DateParserType2 = 2,
    }

    public enum UrlTransformer
    {
        UrlTransformerType0 = 0,
        UrlTransformerType1 = 1,
        UrlTransformerType2 = 2,
    }

    public enum SearchState
    {
        Ping,
        Search,
        Match,
        Save,
        Complete,
    }
}
