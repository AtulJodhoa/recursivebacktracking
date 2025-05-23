﻿using System;
using System.Collections.Generic;

namespace CoinRepresentation
{
    public class CoinRepresentation
    {
        public static long Solve(long sum)
        {
            //calculate maximum power of 2 (k) required to cover the given sum
            //greedy heuristic
            int maxPower = (int)Math.Floor(Math.Log(sum, 2));
            var hashMapMemo = new Dictionary<(long, int), long>();

            return CountWays(sum, maxPower, hashMapMemo);
        }

        // Recursive Backtracking with Memoization 
        private static long CountWays(long remainingSum, int k, Dictionary<(long, int), long> hashMapMemo)
        {
            //base case matched
            if (remainingSum == 0)
            {
                return 1;
            }

            //kill nodes if they exceed the remaining sum - bounding function
            if (remainingSum < 0 || k < 0)
            {
                return 0;
            }

            var key = (remainingSum, k);

            // Check if result is already cached in memoization dictionary
            if (hashMapMemo.ContainsKey(key))
                return hashMapMemo[key];

            // Calculate the coin value for 2^k
            long coinValue = 1L << k;
            long totalWays = 0;

            // We can use 0, 1, or 2 coins of the current coin value (since there are only two coins available for each value)
            for (int numOfCoinsToUse = 0; numOfCoinsToUse < 3; numOfCoinsToUse++)
            {
                long newRemaining = remainingSum - (numOfCoinsToUse * coinValue);

                //Bounding condition reached. Backtrack
                if (newRemaining < 0) break;

                // Else dive deeper in the Space State Tree
                totalWays += CountWays(newRemaining, k - 1, hashMapMemo);
            }

            // Store the result of the current subproblem in memoization
            hashMapMemo[key] = totalWays;
            return totalWays;
        }
    }
}
