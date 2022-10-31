namespace BoyerMooreSearch
{
    public class Search
    {
        public int FullSearch(string text, string mask)
        {
            int textIndex = 0;
            while(textIndex <= text.Length - mask.Length)
            {
                int maskIndex = 0;
                while(maskIndex < mask.Length && text[textIndex + maskIndex] == mask[maskIndex])
                {
                    maskIndex++;
                }
                if(maskIndex == mask.Length)
                {
                    return textIndex;
                }
                textIndex++;
            }
            return -1;
        }
        public int SearchBMH(string text, string mask)
        {
            int[] shift = CreateShift(mask);
            int textIndex = 0;
            while(textIndex <= text.Length - mask.Length)
            {
                int maskIndex = mask.Length - 1;
                while(maskIndex >=0 && text[textIndex + maskIndex] == mask[maskIndex])
                {
                    maskIndex--;
                }
                if(maskIndex == -1)
                {
                    return textIndex;
                }
                textIndex += shift[text[textIndex + mask.Length - 1]];
            }
            return -1;
        }
        public int[] CreateShift(string mask)
        {
            int[] shift = new int[128];
            for(int i = 0; i < shift.Length; i++)
            {
                shift[i] = mask.Length;
            }
            for(int m = 0; m < mask.Length - 1; m++)
            {
                shift[mask[m]] = mask.Length - m - 1;
            }
            return shift;
        }

        public int SearchBMHSuffix(string text, string mask)
        {
            var shiftMap = ExtractShift(mask);
            int[] shiftSimple = CreateShift(mask);
            int textIndex = 0;
            while (textIndex <= text.Length - mask.Length)
            {
                int maskIndex = mask.Length - 1;
                while (maskIndex >= 0 && text[textIndex + maskIndex] == mask[maskIndex])
                {
                    maskIndex--;
                }
                if (maskIndex == -1)
                {
                    return textIndex;
                }
                var checkShiftMap = shiftMap.ContainsKey(mask.Substring(maskIndex + 1));
                if(checkShiftMap)
                {
                    textIndex += shiftMap[mask.Substring(maskIndex + 1)];
                }
                else
                {
                    textIndex += shiftSimple[text[textIndex + mask.Length - 1]];
                }
                
            }
            return -1;
        }
        public Dictionary<string, int> ExtractShift(string mask)
        {
            var shiftMap = new Dictionary<string, int>();
            var maskLength = mask.Length;
            for (int i = mask.Length - 1; i >= 0; i--)
            {
                var pattern = mask.Substring(i);
                var modMask = mask.Substring(0, i);
                var patternIndex = modMask.IndexOf(pattern);
                if(patternIndex == -1 | patternIndex == i)
                {
                    shiftMap[pattern] = maskLength;
                }
                else
                {
                    shiftMap[pattern] = i - patternIndex;
                }
            }
            return shiftMap;
        }
    }
}
