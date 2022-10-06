// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Security.Models
// {
    public class DesPostResponse
    {
        public string Data { get; set; }
        public List<string> LeftParts { get; set; }
        public List<string> RightParts { get; set; }
        public string AfterInitialPermutation { get; set; }
        public int StatusCode { get; set; }

        public DesPostResponse(string data, List<string> leftParts, List<string> rightParts, string afterInitialPermutation, int code)
        {
            this.AfterInitialPermutation = afterInitialPermutation;
            this.Data = data;
            this.LeftParts  = leftParts;
            this.RightParts = rightParts;
            this.StatusCode = code;
        }
    }
// }