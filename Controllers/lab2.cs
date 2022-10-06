using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[ApiController]
[Route("[controller]")]
public class Lab2 : ControllerBase
{
    //variables for algorithm
    private string LeftPart = "";
    private string RightPart = "";
    //variables for response
    private List<string> LeftParts = new List<string>();
    private List<string> RightParts = new List<string>();

    private DesUtilities DesConsts = new DesUtilities();
    private DesFunc DesFunctions = new DesFunc();


    private string DesEncryption(string pt, List<string> rkb, List<string> rk, DesPostResponse obj)
    {
        pt = DesFunctions.HexToBin(pt);

        pt = DesFunctions.Permute(pt, DesConsts.InitialPermutation, 64);
        obj.AfterInitialPermutation = DesFunctions.BinToHex(pt);
        LeftPart = pt.Substring(0, 32);
        RightPart = pt.Substring(32, 32);

        LeftParts.Add(DesFunctions.BinToHex(LeftPart));
        RightParts.Add(DesFunctions.BinToHex(RightPart));

        //16 roudes
        for(int  i = 0; i < 16; i++)
        {
            string RightExpanded = DesFunctions.Permute(RightPart, DesConsts.ExpansionDBox, 48);
            string x = DesFunctions.Xor(rkb[i], RightExpanded);
            //S-boxes
            string op = "";
            for(int  j = 0; j < 8; j++)
            {
                int row = 2 * (x[j * 6] - '0')
                      + (x[j * 6 + 5] - '0');
                int col = 8 * (x[j * 6 + 1] - '0')
                        + 4 * (x[j * 6 + 2] - '0')
                        + 2 * (x[j * 6 + 3] - '0')
                        + (x[j * 6 + 4] - '0');
                int val = DesConsts.SBox[j, row, col];
                op += Convert.ToChar(val / 8 + '0');
                val = val % 8;
                op += Convert.ToChar(val / 4 + '0');
                val = val % 4;
                op += Convert.ToChar(val / 2 + '0');
                val = val % 2;
                op += Convert.ToChar(val + '0');
            }

            op = DesFunctions.Permute(op, DesConsts.Per, 32);
            x = DesFunctions.Xor(op, LeftPart);
            LeftPart = x;

            if(i != 15)
            {
                string Temp = LeftPart;
                LeftPart = RightPart;
                RightPart = Temp;
            }
            Console.WriteLine("Round " + i + " " + DesFunctions.BinToHex(LeftPart) + " " + DesFunctions.BinToHex(RightPart) + " " + rk[i]);
            LeftParts.Add(DesFunctions.BinToHex(LeftPart));
            RightParts.Add(DesFunctions.BinToHex(RightPart));
        }

        string CombinedString =  LeftPart + RightPart;

        string EncodedString = DesFunctions.BinToHex(DesFunctions.Permute(CombinedString, DesConsts.FinalPermutation, 64));

        return EncodedString;
    }

    [HttpPost(Name = "Lab2")]
    public IActionResult Post(StringForEncryotion Input, Boolean Decrypt)
    {

        Input.Key = DesFunctions.HexToBin(Input.Key);
        Input.Key = DesFunctions.Permute(Input.Key, DesConsts.KeyP, 56);


        string Left = Input.Key.Substring(0, 28);
        string Right = Input.Key.Substring(28, 28);

        List<string> rkb = new List<string>();
        List<string> rk = new List<string>();

        DesPostResponse obj = new DesPostResponse("", LeftParts, RightParts, "", 0);

        for(int i = 0; i < 16; i++)
        {
            Left = DesFunctions.LeftShift(Left, DesConsts.ShiftTable[i]);
            Right = DesFunctions.LeftShift(Right, DesConsts.ShiftTable[i]);

            string CombineKey = Left + Right;

            string RoundKey = DesFunctions.Permute(CombineKey, DesConsts.KeyComp, 48);
            
            rkb.Add(RoundKey);
            rk.Add(DesFunctions.BinToHex(RoundKey));

        }

        if(Decrypt) 
        {
            rkb.Reverse();
            rk.Reverse();
        }

        obj.Data = DesEncryption(Input.PlainText, rkb, rk, obj);
        obj.LeftParts = LeftParts;
        obj.RightParts = RightParts;
        obj.StatusCode = 200;

        return Ok(obj);
    }

}

