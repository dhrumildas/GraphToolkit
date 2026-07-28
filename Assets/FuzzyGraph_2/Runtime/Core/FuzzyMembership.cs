using System;
using UnityEngine;

namespace FuzzyGraph2.Runtime
{
    public static class FuzzyMembership
    {
        public static float High(float val, float start, float full)
        {
            ValidateIncreasingBounds(start, full);

            if (val <= start)
                return 0f;
            if( val >= full )
                return 1f;

            float membership = (val - start) / (full - start);
            return FuzzyMath.Clamp(membership);
        }

        private static void ValidateIncreasingBounds(float start, float full)
        {
            if(full <= start)
            {
                throw new ArgumentException($"The ending boundary ({full}) must be > the starting boundary ({start})");
            }
        }
    }
}
