namespace jh
{
    public static class AppSettings
    {
        public static string Pingkeyword = "info";
        public static string KeywordReplacePattern = "{keyword}";
        public static string PageIndexReplacePattern = "{pageindex}";
        public static int MaxPagesToScan = 1;
        public static int HttpRequestTimeout = 3;
        public static int HttpRequestMaxNbrOfAttempts = 3;
        public static int MaxJoffersToPing = 3;
    }
}
