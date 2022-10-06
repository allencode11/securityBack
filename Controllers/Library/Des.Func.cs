using System.Text;

public class DesFunc
{
    private static readonly Dictionary<char, string> hexToBinDictionary = new Dictionary<char, string> {
        { '0', "0000" },
        { '1', "0001" },
        { '2', "0010" },
        { '3', "0011" },
        { '4', "0100" },
        { '5', "0101" },
        { '6', "0110" },
        { '7', "0111" },
        { '8', "1000" },
        { '9', "1001" },
        { 'a', "1010" },
        { 'b', "1011" },
        { 'c', "1100" },
        { 'd', "1101" },
        { 'e', "1110" },
        { 'f', "1111" }
    };

    private static readonly Dictionary<string, char> binToHexDictionary = new Dictionary<string, char> {
        { "0000" , '0' },
        { "0001" , '1' },
        { "0010" , '2' },
        { "0011" , '3' },
        { "0100" , '4' },
        { "0101" , '5' },
        { "0110" , '6' },
        { "0111" , '7' },
        { "1000" , '8' },
        { "1001" , '9' },
        { "1010" , 'a' },
        { "1011" , 'b' },
        { "1100" , 'c' },
        { "1101" , 'd' },
        { "1110" , 'e' },
        { "1111" , 'f' }
    };

    public string HexToBin(string input)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in input)
        {
            result.Append(hexToBinDictionary[char.ToLower(c)]);
        }

        return result.ToString();
    }

    public string BinToHex(string input)
    {
        StringBuilder result = new StringBuilder();
        for(int i = 0; i < input.Length; i += 4)
        {
            string set = input[i].ToString() + input[i + 1] + input[i + 2] + input[i + 3];
            result.Append(binToHexDictionary[set]);
        }
        return result.ToString();
    }

    public string Permute(string str, int[] arr, int n)
    {
        string Per = "";
        for(int i = 0; i < n; i++)
        {
            Per += str[arr[i] - 1];
        }
        return Per;
    }

    public string LeftShift(string str, int shifts)
    {
        string Response = "";
        for(int i = 0; i < shifts; i++)
        {
            for(int j = 1; j < 28; j++)
            {
                Response += str[j];
            }

            Response += str[0];
            str = Response;
            Response = "";
        }
        return str;
    }

    public string Xor(string a, string b)
    {
        string Response = "";
        for(int i = 0; i < a.Length; i++)
        {
            if(a[i] == b[i])
                Response += "0";
            else
                Response += "1";
        }

        return Response;
    }

}