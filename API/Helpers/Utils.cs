using API.Entities;
using System;

namespace API.Helpers
{
    public static class Utils
    {
        public static string GenerateRandomNumber(string productType)
        {
            string generatedString = "";
            int randomNumber = 0;
            Random rd = new Random();
            randomNumber = rd.Next(1000000000, 2000000000);
            generatedString = randomNumber.ToString();
            rd = new Random();
            randomNumber = rd.Next(1000000000, 2000000000);
            generatedString = generatedString + randomNumber.ToString();

            if (productType.Equals("CARD")) return generatedString.Substring(0,16);
            else return generatedString.Substring(0,9);
        }
    }
}
