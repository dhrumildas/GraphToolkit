using System;
using NUnit.Framework;
using FuzzyGraph.Runtime;
using FuzzyGraph2.Runtime;

namespace FuzzyGraph2.Tests.EditMode
{
    public class WorldStateQueryValueSourceTests
    {
        private const float Tolerance = 0.0001f;

        [Test]
        public void Source_ReadsFloatValue()
        {
            WorldStateQuery query =
                new WorldStateQuery();

            query.Set(
                "Vendor.CarpetProximity",
                FuzzyValue.FromFloat(0.75f));

            WorldStateQueryValueSource source =
                new WorldStateQueryValueSource(query);

            bool found = source.TryGetFloat(
                "Vendor.CarpetProximity",
                out float value);

            Assert.IsTrue(found);
            Assert.AreEqual(0.75f, value, Tolerance);
        }

        [Test]
        public void Source_ConvertsIntegerValueToFloat()
        {
            WorldStateQuery query =
                new WorldStateQuery();

            query.Set(
                "Guard.Relationship",
                FuzzyValue.FromInt(65));

            WorldStateQueryValueSource source =
                new WorldStateQueryValueSource(query);

            bool found = source.TryGetFloat(
                "Guard.Relationship",
                out float value);

            Assert.IsTrue(found);
            Assert.AreEqual(65f, value, Tolerance);
        }

        [Test]
        public void Source_MissingValue_ReturnsFalse()
        {
            WorldStateQueryValueSource source =
                new WorldStateQueryValueSource(
                    new WorldStateQuery());

            bool found = source.TryGetFloat(
                "Missing.Value",
                out float value);

            Assert.IsFalse(found);
            Assert.AreEqual(0f, value, Tolerance);
        }

        [Test]
        public void Source_BooleanValue_ReturnsFalse()
        {
            WorldStateQuery query =
                new WorldStateQuery();

            query.Set(
                "Player.HasFlowers",
                FuzzyValue.FromBool(true));

            WorldStateQueryValueSource source =
                new WorldStateQueryValueSource(query);

            bool found = source.TryGetFloat(
                "Player.HasFlowers",
                out _);

            Assert.IsFalse(found);
        }

        [Test]
        public void Source_UsesQueryBuilderLiveOverride()
        {
            PersistentContext context =
                new PersistentContext();

            context.Set(
                "Guard.Relationship",
                FuzzyValue.FromInt(20));

            QueryBuilder queryBuilder =
                new QueryBuilder(context);

            queryBuilder.SetLive(
                "Guard.Relationship",
                FuzzyValue.FromFloat(65f));

            WorldStateQuery query =
                queryBuilder.Build();

            WorldStateQueryValueSource source =
                new WorldStateQueryValueSource(query);

            FuzzySetDefinition friendly =
                FuzzySetDefinition.High(
                    "Friendly",
                    start: 50f,
                    full: 80f);

            FuzzyVariableDefinition relationship =
                new FuzzyVariableDefinition(
                    id: "Guard.Relationship",
                    displayName: "Guard Relationship",
                    minimum: 0f,
                    maximum: 100f,
                    sets: new[] { friendly });

            FuzzyIsStatement statement =
                new FuzzyIsStatement(
                    relationship,
                    friendly);

            float result = statement.Evaluate(source);

            // QueryBuilder replaces the persistent 20
            // with the current live value of 65.
            //
            // Friendly membership:
            // (65 - 50) / (80 - 50) = 0.5

            Assert.AreEqual(0.5f, result, Tolerance);
        }

        [Test]
        public void Source_NullQuery_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new WorldStateQueryValueSource(null));
        }
    }
}