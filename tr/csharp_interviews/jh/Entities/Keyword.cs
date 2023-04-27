namespace jh.Entities
{
    public class Keyword
    {
        public int KeywordId { get; set; }
        public string Value { get; set; }
        public KeywordTypes KeywordType { get; set; }
        public Keyword()
        {

        }

        public Keyword(int keywordId, string value, KeywordTypes keywordType)
        {
            KeywordId = keywordId;
            Value = value;
            KeywordType = keywordType;
            //if (IsComplexe && isRequired)
            //    throw new KeywordCannotBeRequiredException();

        }

        public bool IsComplexe
        {
            get { return Value.Contains(" "); }
        }
    }

    public enum KeywordTypes
    {
        Title,
        Description,
        NiceToHave,
        AvoidInTitle,
        AvoidInDescription
    }
}
