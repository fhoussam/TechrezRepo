using System;
using System.Collections.Generic;
using System.Text;

namespace jh
{
    public class PathException : Exception { }
    public class PingException : Exception { }
    public class TagMismatchException : Exception { }
    public class KeywordCannotBeRequiredException : Exception { }
    public class ProviderNotFoundException : PingException { }
    public class ListUrlPingException : PingException { }
    public class DescriptionPingException : PingException { }
    public class ListUrlPathException : PathException { }
    public class DatePathException : PathException { }
    public class TitlePathException : PathException { }
    public class ParseDateException : Exception { }
    public class DescriptionPathException : PathException { }
    public class EmptyPageException : Exception { }
    public class DuplicatedEntityResultException : Exception { }
    public class EntityResultNoLongerAvailableException : Exception { }
}
