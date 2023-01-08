namespace PackMarket.Extentions
{
    public class StringTransliterate
    {
        private List<TranslitSymbol> translitSymbols;
        private string inputChars = "а:a,б:b,в:v,г:g,д:d,е:e,ё:yo,ж:zh,з:z,и:i,й:j,к:k,л:l,м:m,н:n,о:o,п:p,р:r,с:s,т:t,у:u,ф:f,х:h,ц:c,ч:ch,ш:sh,щ:shh,ъ:,ы:y,ь:',э:e,ю:yu,я:ya";
        public StringTransliterate()
        {
            translitSymbols= new List<TranslitSymbol>();
            foreach (string item in inputChars.Split(","))
            {
                string[] symbols = item.Split(":");
                translitSymbols.Add(new TranslitSymbol
                {
                    SymbolRus = symbols[0].ToLower(),
                    SymbolEng= symbols[1].ToLower()
                });
            }
        }
        public string TranslateToUrl(string str)
        {
            str= str.ToLower();
            var result = String.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                var symbol = translitSymbols.FirstOrDefault(x => x.SymbolRus == str[i].ToString());
                if (symbol.SymbolRus == str[i].ToString())
                {
                    result += symbol.SymbolEng;
                }
                else if(symbol.SymbolRus==" ")
                {
                    result += "-";
                }
                else
                {
                    result += str[i];
                }
                   
            }
            return result;
        }
        public string TranslateToRussian(string str)
        {
            str= str.ToLower();
            var result = String.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                var symbol = translitSymbols.FirstOrDefault(x => x.SymbolEng == str[i].ToString());
                if (symbol.SymbolEng == str[i].ToString())
                {
                    result += symbol.SymbolRus;
                }
                else if (symbol.SymbolEng == "-")
                {
                    result += " ";
                }
                else
                {
                    result += str[i];
                }
            }
            return result;
        }
        public string TranslateToRussianWithCapl(string str)
        {
            str = str.ToLower();
            var result = String.Empty;
            for (int i = 0; i < str.Length; i++)
            {
                var symbol = translitSymbols.FirstOrDefault(x => x.SymbolEng == str[i].ToString());
                if (symbol.SymbolEng == str[i].ToString())
                {
                    result += symbol.SymbolRus;
                }
                else
                {
                    result += str[i];
                }
            }
            result = result.Substring(0, 1).ToUpper() + result.Substring(1);
            return result;
        }
    }
    public struct TranslitSymbol
    {
        public string SymbolRus;
        public string SymbolEng;
    }
}
