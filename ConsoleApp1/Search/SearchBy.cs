namespace OP8.Search;

public abstract class SearchBy<Type>
{
    public abstract List<Type> SearchByKeyword(List<Type> items, string keyword);

    protected bool IsMatch(string text, string keyword)
    {
        if (string.IsNullOrEmpty(keyword)) return true;
        if (string.IsNullOrEmpty(text) || text.Length < keyword.Length) return false;

        string lowerText = text.ToLower();
        string lowerKeyword = keyword.ToLower();

        for (int i = 0; i <= lowerText.Length - lowerKeyword.Length; i++)
        {
            bool match = true;

            for (int j = 0; j < lowerKeyword.Length; j++)
            {
                if (lowerText[i + j] != lowerKeyword[j])
                {
                    match = false;
                    break;
                }
            }

            if (match)
            {
                return true;
            }
        }

        return false;
    }
}