﻿namespace Lender.Slos.Financial
{
    using System;
    using System.Globalization;

    public static class Calculator
    {
        public static readonly int MaxTermInMonths = 360;

        public static readonly decimal MinPrincipalAmount = 1000m;
        public static readonly decimal MaxPrincipalAmount = 1000000m;

        public static readonly decimal PeriodsPerYear = 12m;

        public static decimal ComputeRatePerPeriod(
            decimal annualPercentageRate)
        {
            if (annualPercentageRate < 0.01m ||
                annualPercentageRate >= 20.0m)
            {
                throw new InvalidOperationException(string.Format(
                    "AnnualPercentageRate {0}% is not valid",
                    annualPercentageRate));
            }

            var ratePerMonth = (annualPercentageRate / 100m) / PeriodsPerYear;

            return Math.Round(ratePerMonth, 6, MidpointRounding.AwayFromZero);
        }

        public static decimal ComputePaymentPerPeriod(
            decimal principalAmount,
            decimal ratePerPeriod,
            int termInPeriods)
        {
            if (termInPeriods <= 0 || termInPeriods > MaxTermInMonths)
            {
                throw new ArgumentOutOfRangeException("termInPeriods");
            }

            if (ratePerPeriod <= decimal.Zero || ratePerPeriod > 0.017m)
            {
                throw new ArgumentOutOfRangeException(string.Format(
                    "RatePerPeriod {0}% is not valid",
                    ratePerPeriod));
            }

            if (principalAmount < MinPrincipalAmount || 
                principalAmount >= MaxPrincipalAmount)
            {
                throw new InvalidOperationException(string.Format(
                    "Principal {0} is not valid", 
                    principalAmount.ToString("C", new CultureInfo("EN-us"))));
            }

            var exponentBase = Convert.ToDouble(decimal.One + ratePerPeriod);
            var exponent = Convert.ToDecimal(Math.Pow(exponentBase, -1 * termInPeriods));

            var payment = (ratePerPeriod * principalAmount) / (1m - exponent);
            payment = Math.Round(payment, 2, MidpointRounding.AwayFromZero);

            return payment;
        }
    }
}
