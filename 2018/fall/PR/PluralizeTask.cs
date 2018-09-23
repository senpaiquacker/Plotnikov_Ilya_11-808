namespace Pluralize
{
    public static class PluralizeTask
    {
        public static string PluralizeRubles(int count)
        {
            string argum;
            if (((count % 100 > 10) && (count % 100 < 20)) || (count % 10 > 4) || count % 10 == 0)
            {
                argum = "лей";
            }
            else if(count % 10 != 1)
            {
                argum = "ля";
            }
            else
            {
                argum = "ль";
            }
			return "руб"+argum;
		}
	}
}