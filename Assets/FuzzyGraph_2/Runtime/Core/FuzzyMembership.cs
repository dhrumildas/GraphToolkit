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

        public static float Range(float val, float start, float fullStart, float fullEnd, float end)
        {
            ValidateRangeBounds(start, fullStart, fullEnd, end);

            if (val <= start || val >= end)
                return 0f;

            if (val >= fullStart && val <= fullEnd)
                return 1f;

            if (val < fullStart)
            {
                float rising = (val - start) / (fullStart - start);

                return FuzzyMath.Clamp(rising);
            }

            float falling = (end - val) / (end - fullEnd);

            return FuzzyMath.Clamp(falling);
        }

        private static void ValidateRangeBounds(float start, float fullStart, float fullEnd, float end)
        {
            if (fullStart <= start)
            {
                throw new ArgumentException(
                    $"The full-start boundary ({fullStart}) must be > the start boundary ({start}).");
            }

            if (fullEnd < fullStart)
            {
                throw new ArgumentException(
                    $"The full-end boundary ({fullEnd}) must be >= the full-start boundary ({fullStart}).");
            }

            if (end <= fullEnd)
            {
                throw new ArgumentException(
                    $"The end boundary ({end}) must be > the full-end boundary ({fullEnd}).");
            }
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
