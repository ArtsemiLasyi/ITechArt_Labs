namespace DataAccess.Services
{
    public static class SearchService
    {
        private const string SEPARATOR = " ";

        public static bool Contains(string str, string substr)
        {
            str = SetToDefaultState(str);
            substr = SetToDefaultState(str);
            string[] strWords = str.Split(SEPARATOR);
            string[] substrWords = substr.Split(SEPARATOR);
            foreach (string strWord in strWords)
            {
                foreach (string substrWord in substrWords)
                {
                    if (strWord.Contains(substrWord))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string SetToDefaultState(string str)
        {
            return str.Trim().ToLower();
        }
    }
}