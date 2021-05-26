using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;

namespace NHSNumberValidation
{
    public class NHSNumberValidation
    {
        /// <summary>
        /// The Check Number.  This is the Last number of the NHS Number
        /// </summary>
        private int checkNumber;
        /// <summary>
        /// The NHS Number.  This is a String
        /// </summary>
        private string NHSNumber;
        /// <summary>
        /// A array of multipliers
        /// </summary>
        private int[] multipliers;
        /// <summary>
        /// Is the NHS number valid
        /// </summary>
        private bool isValid;

        /// <summary>
        /// Validates a NHS Number
        /// </summary>
        /// <param name="NHSNumber">String</param>
        public  void NHSNumberValdiation(string NHSNumber)
        {
            /// Remove any white space characters from the NHSNumber
            this.NHSNumber = Regex.Replace(NHSNumber, @"\s", "", RegexOptions.IgnoreCase | RegexOptions.Multiline); ;

            /// Create and populate the multipler array
            this.multiplers = new int[9];
            for (int i = 0; i < multipliers.Length; i++)
            {
                this.multipliers[i] = 10 - i;
            }

            /// Make sure the input is valid
            this.validateInput();
            if (this.isValid)
            {
                /// Validate the NHSNumber
                this.validateNHSNumber();
            }
        }
        /// <summary>
        /// Validates the input
        /// Makes sure that the NHSNumber is numeric
        /// </summary>
        protected void validateInput()
        {
            Match match = Regex.Match(this.NHSNumber, "(\\d+)");
            if (match.Success)
            {
                this.isValid = true;
            }
            else
            {
                this.isValid = false;
            }
        }
        /// <summary>
        /// Validates the NHSNumber
        /// </summary>
        protected void validateNHSNumber()
        {
            /// The sum of the multipliers
            int currentSum = 0;

            /// Get the check number
            this.checkNumber = Convert.ToInt16(this.NHSNumber[this.NHSNumber.Length - 1].ToString());

            /// The remainder after the sum calculations
            int remainder = 0;

            /// The total to be checked against the check number
            int total = 0;

            /// Loop over each number in the string and calculate the current sum
            for (int i = 0; i < this.NHSNumber.Length - 1; i++)
            {
                currentSum += (Convert.ToInt16(this.NHSNumber[i].ToString()) * this.multipliers[i]);
            }

            /// Calculate the remainder and get the total
            remainder = currentSum % 11;
            total = 11 - remainder;

            /// Now we have our total we can validate it against the check number
            if (total.Equals(11))
            {
                total = 0;
            }

            if (total.Equals(10))
            {
                this.isValid = false;
            }

            if (total.Equals(this.checkNumber))
            {
                this.isValid = true;
            }
            else
            {
                this.isValid = false;
            }
        }
        /// <summary>
        /// Gets the validation status
        /// </summary>
        /// <returns>Bool True if valid or False otherwise</returns>
        public bool getIsValid()
        {
            return this.isValid;
        }

        /// <summary>
        /// Returns a formatted version of the NHS number in 3-3-4 grouping
        /// </summary>
        /// <returns></returns>
        public string FormattedNumber()
        {
            return Regex.Replace(this.NHSNumber, @"(\d{3})(\d{3})(\d{4,})", "${1} ${2} ${3}", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        }
    }

}
