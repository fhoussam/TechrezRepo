namespace jh.Entities
{
    public class Keyword
    {
        public int KeywordId { get; set; }
        public string Value { get; set; }
        public bool IsRequired { get; set; }
        public Keyword()
        {

        }

        public Keyword(int keywordId, string value, bool isRequired = true)
        {
            KeywordId = keywordId;

            Value = value;

            if (IsComplexe && isRequired)
                throw new KeywordCannotBeRequiredException();

            IsRequired = isRequired == true;
        }

        public bool IsComplexe
        {
            get { return Value.Contains(" "); }
        }
    }
}
