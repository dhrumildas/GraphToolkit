using System;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
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

        public static float Low(float val, float full, float end)
        {
            ValidateIncreasingBounds(full, end);

            if (val <= full)
                return 1f;

            if (val >= end)
                return 0f;

            float membership = (end - val) / (end - full);

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
