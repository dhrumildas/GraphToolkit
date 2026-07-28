namespace FuzzyGraph2.Runtime
{
    public static class FuzzyMath
    {
        public static float Clamp(float val)
        {
            if (val < 0f)
                return 0f;

            if (val > 1f)
                return 1f;

            return val;
        }

        public static float AND(float l, float r)
        {
            l = Clamp(l);
            r = Clamp(r);
            return l * r;
        }

        public static float OR(float l, float r)
        {
            l = Clamp(l);
            r = Clamp(r);
            return l + r - (l * r);
        }

        public static float NOT(float val)
        {
            val = Clamp(val);
            return 1f - val;
        }

        public static float Smooth(float val)
        {
            float t = Clamp(val);
            return t * t * (3f - 2f * t);
        }
    }
}