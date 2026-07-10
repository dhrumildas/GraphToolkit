using System;
using UnityEngine;

namespace FuzzyGraph.Runtime
{
    [Serializable]
    public class RuntimeCriteria
    {
        public string fieldKey;
        public CriteriaOperator criteriaOperator;
        public FuzzyValue expectedVal;
        public float weight = 1f;
        public FuzzyEvaluationMode evalMode = FuzzyEvaluationMode.Crisp;

        //Used by FUzzyLow, FuzzyHigh and FuzzyRange
        public float fuzzyMin = 0f;
        public float fuzzyMax = 1f;

        // Used by FuzzyRange to control how quickly membership
        // decreases outside the preferred range

        [Min(0f)]
        public float fuzzyLimit = 1f;

        public float GetMembership(WorldStateQuery query)
        {
            bool exists = query.TryGet(fieldKey, out FuzzyValue actualVal);

            //exists and doesNotExists are binary vals
            if(criteriaOperator == CriteriaOperator.Exists)
                return exists ? 1f : 0f;

            if (criteriaOperator == CriteriaOperator.DoesNotExist)
                return exists ? 0f : 1f;

            if(!exists)
                return 0f;

            if(evalMode == FuzzyEvaluationMode.Crisp)
                return EvaluateCrisp(actualVal) ? 1f : 0f;

            if(!TryGetNumericVal(actualVal, out float numericVal))
                return 0f;
            return evalMode switch
            {
                FuzzyEvaluationMode.FuzzyLow => EvaluateFuzzyLow(numericVal),
                FuzzyEvaluationMode.FuzzyHigh => EvaluateFuzzyHigh(numericVal),
                FuzzyEvaluationMode.FuzzyRange => EvaluateFuzzyRange(numericVal),
                _ => 0f
            };
        }

        public float WeightedContribution(WorldStateQuery query)
        {
            float membership = GetMembership(query);
            float safeWeight = Mathf.Max(0f, weight);

            return membership * safeWeight;
        }

        private float EvaluateFuzzyRange(float x)
        {
            float rangeMin = Mathf.Min(fuzzyMin, fuzzyMax);
            float rangeMax = Mathf.Max(fuzzyMin, fuzzyMax);

            //full membership inside this preferred range
            if (x >= rangeMin && x <= rangeMax)
                return 1f;

            float safeLimit = Mathf.Max(0.0001f, fuzzyLimit);

            if (x < rangeMin)
                return Mathf.InverseLerp(rangeMin - safeLimit, rangeMin, x);

            return 1f - Mathf.InverseLerp(rangeMax, rangeMax + safeLimit, x);

        }

        private float EvaluateFuzzyHigh(float x)
        {
            //0 membership =< fuzzyMin
            //Full Membership => fuzzyMax
            if (fuzzyMax <= fuzzyMin)
                return x >= fuzzyMax ? 1f : 0f;

            return Mathf.InverseLerp(fuzzyMin,fuzzyMax, x);
        }

        private float EvaluateFuzzyLow(float x)
        {
            //full memebership =< fuzzyMin
            //0 membership => fuzzyMax
            if (fuzzyMax <= fuzzyMin)
                return x <= fuzzyMin ? 1f : 0f;
            return 1f - Mathf.InverseLerp(fuzzyMin,fuzzyMax,x);
        }

        private bool TryGetNumericVal(FuzzyValue x, out float y)
        {
            switch(x.type)
            {
                case FuzzyValueType.Int:
                    y = x.intVal;
                    return true;

                case FuzzyValueType.Float:
                    y = x.floatVal;
                    return true;
                default:
                    y = 0f;
                    return false;
            }
        }

        private bool EvaluateCrisp(FuzzyValue actualVal)
        {
            if(actualVal.type != expectedVal.type) return false;

            return actualVal.type switch
            {
                FuzzyValueType.Bool => CompareBool(actualVal.boolVal, expectedVal.boolVal),
                FuzzyValueType.Int => CompareInt(actualVal.intVal, expectedVal.intVal),
                FuzzyValueType.Float => CompareFloat(actualVal.floatVal, expectedVal.floatVal),
                FuzzyValueType.String => CompareString(actualVal.stringVal, expectedVal.stringVal),
                _ => false
            };
        }

        public bool Matches(WorldStateQuery query)
        {
            //bool exists = query.TryGet(fieldKey, out FuzzyValue actualVal);
            //if(criteriaOperator == CriteriaOperator.Exists)
            //{
            //    return exists;
            //}

            //if(criteriaOperator == CriteriaOperator.DoesNotExist)
            //{
            //    return !exists;
            //}

            //if(!exists)
            //{
            //    return false;
            //}

            //if(actualVal.type != expectedVal.type)
            //{
            //    return false;
            //}

            //return actualVal.type switch
            //{
            //    FuzzyValueType.Bool => CompareBool(actualVal.boolVal, expectedVal.boolVal),
            //    FuzzyValueType.Int => CompareInt(actualVal.intVal, expectedVal.intVal),
            //    FuzzyValueType.Float => CompareFloat(actualVal.floatVal, expectedVal.floatVal),
            //    FuzzyValueType.String => CompareString(actualVal.stringVal, expectedVal.stringVal),
            //    _ => false
            //};

            return GetMembership(query) > 0f;
        }

        private bool CompareBool(bool actual, bool expected)
        {
            return criteriaOperator switch
            {
                CriteriaOperator.Equals => actual == expected,
                CriteriaOperator.NotEquals => actual != expected,
                _ => false
            };
        }

        private bool CompareInt(int actual, int expected)
        {
            return criteriaOperator switch
            {
                CriteriaOperator.Equals => actual == expected,
                CriteriaOperator.NotEquals => actual != expected,
                CriteriaOperator.GreaterThan => actual > expected,
                CriteriaOperator.GreaterThanOrEqual => actual >= expected,
                CriteriaOperator.LessThan => actual < expected,
                CriteriaOperator.LessThanOrEqual => actual <= expected,
                _ => false
            };
        }

        private bool CompareFloat(float actual, float expected)
        {
            return criteriaOperator switch
            {
                CriteriaOperator.Equals => Math.Abs(actual - expected) < 0.0001f,
                CriteriaOperator.NotEquals => Math.Abs(actual - expected) >= 0.0001f,
                CriteriaOperator.GreaterThan => actual > expected,
                CriteriaOperator.GreaterThanOrEqual => actual >= expected,
                CriteriaOperator.LessThan => actual < expected,
                CriteriaOperator.LessThanOrEqual => actual <= expected,
                _ => false
            };
        }

        private bool CompareString(string actual, string expected)
        {
            return criteriaOperator switch
            {
                CriteriaOperator.Equals => actual == expected,
                CriteriaOperator.NotEquals => actual != expected,
                _ => false
            };
        }
    }
}