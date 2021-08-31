namespace BusinessLogic.Services
{
    public class SearchService
    {
        private const string SEPARATOR = " ";

        public bool Contains(string str, string substr)
        {
            str = SetToDefaultState(str);
            substr = SetToDefaultState(str);
            string[] strWords = str.Split(SEPARATOR);
            string[] substrWords = substr.Split(SEPARATOR);
            foreach(string strWord in strWords)
            {
                 foreach(string substrWord in substrWords)
                 {
                    if (strWord.Contains(substrWord))
                    {
                        return true;
                    }
                 }
            }
            return false;
        }

        private string SetToDefaultState(string str)
        {
            return str.Trim().ToLower();
        }
    }
}
