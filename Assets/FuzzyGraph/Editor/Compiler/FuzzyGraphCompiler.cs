using System.Collections.Generic;
using System.Linq;
using FuzzyGraph.Runtime;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace FuzzyGraph.Editor
{
    public static class FuzzyGraphCompiler
    {
        public static RuntimeFuzzyGraph Compile(
            FuzzyGraphAsset graph)
        {
            RuntimeFuzzyGraph runtimeGraph = ScriptableObject.CreateInstance<RuntimeFuzzyGraph>();  //to create the runtime graph instance/asset

            runtimeGraph.rules = new List<RuntimeRule>();

            if (graph == null)
            {
                Debug.LogError("FuzzyGraphCompiler failed: graph was null.");

                return runtimeGraph;
            }

            foreach (EventNode eventNode in graph.GetNodes().OfType<EventNode>())       //get all the event nodes in the graph and iterate through them
            {
                string eventID =GetOptionValue<string>(eventNode,EventNode.eventIDOption,string.Empty);     //read the eventID option from the event node

                IEnumerable<RuleNode> connectedRules =GetConnectedNodesFromOutput<RuleNode>(eventNode, EventNode.rulesOutPort);     //get all the rule nodes connected to the event node's output port

                foreach (RuleNode ruleNode in connectedRules)
                {
                    RuntimeRule runtimeRule = ConvertRuleNode(ruleNode, eventID);   //convert the rule node to a runtime rule and add it to the runtime graph's rules list

                    runtimeGraph.rules.Add(runtimeRule);
                }
            }

            Debug.Log($"FuzzyGraphCompiler compiled {runtimeGraph.rules.Count} runtime rule(s).");

            return runtimeGraph;
        }

        private static RuntimeRule ConvertRuleNode(RuleNode ruleNode,string eventID)
        {
            RuntimeRule runtimeRule = new RuntimeRule();        // create a new runtime rule instance from node data

            runtimeRule.eventID = eventID;
            runtimeRule.ruleID = GetOptionValue<string>(ruleNode,RuleNode.ruleIDOption,"unnamed_rule");
            runtimeRule.priority = GetOptionValue<int>(ruleNode,RuleNode.priorityOption,0);
            runtimeRule.Criteria = GetConnectedNodesFromInput<CriteriaNode>(ruleNode,RuleNode.criteriaInPort).Select(ConvertCriteriaNode).ToList();
            runtimeRule.Consequences =GetConnectedNodesFromOutput<ConsequenceNode>(ruleNode,RuleNode.consequencesOutPort).Select(ConvertConsequenceNode).ToList();
            runtimeRule.WriteBacks = GetConnectedNodesFromOutput<WriteBackNode>(ruleNode,RuleNode.writeBacksOutPort).Select(ConvertWriteBackNode).ToList();

            return runtimeRule;
        }

        private static RuntimeCriteria ConvertCriteriaNode(CriteriaNode node)
        {
            FuzzyValueType valueType =GetOptionValue(node, CriteriaNode.valTypeOption, FuzzyValueType.Int);

            bool boolVal = GetOptionValue(node, CriteriaNode.expectedBoolOption, false);
            int intVal = GetOptionValue(node,CriteriaNode.expectedIntOption,0);
            float floatVal = GetOptionValue(node,CriteriaNode.expectedFloatOption,0f);
            string stringVal = GetOptionValue(node,CriteriaNode.expectedStringOption,string.Empty);

            return new RuntimeCriteria
            {
                fieldKey =GetOptionValue<string>(node,CriteriaNode.fieldKeyOption,string.Empty),
                criteriaOperator = GetOptionValue(node, CriteriaNode.criteriaOperatorOption, CriteriaOperator.Equals),
                expectedVal = CreateFuzzyValue(valueType, boolVal, intVal, floatVal, stringVal),
                weight = GetOptionValue(node, CriteriaNode.weightOption, 1f),
                evalMode = GetOptionValue(node, CriteriaNode.evalModeOption, FuzzyEvaluationMode.Crisp),
                fuzzyMin = GetOptionValue(node, CriteriaNode.fuzzyMinOption, 0f),
                fuzzyMax = GetOptionValue(node, CriteriaNode.fuzzyMaxOption, 1f),
                fuzzyLimit = GetOptionValue(node, CriteriaNode.fuzzyFallOffOption,1f)
            };
        }

        private static RuntimeConsequence ConvertConsequenceNode(ConsequenceNode node)
        {
            FuzzyValueType valueType = GetOptionValue(node, ConsequenceNode.valTypeOption, FuzzyValueType.String);
            bool boolVal = GetOptionValue(node, ConsequenceNode.boolValOption, false);
            int intVal = GetOptionValue(node, ConsequenceNode.intValOption, 0);
            float floatVal = GetOptionValue(node, ConsequenceNode.floatValOption, 0f);
            string stringVal = GetOptionValue(node, ConsequenceNode.stringValOption, string.Empty);

            return new RuntimeConsequence
            {
                consequenceType = GetOptionValue(node, ConsequenceNode.consequenceTypeOption, ConsequenceType.Dialogue),
                targetKey = GetOptionValue<string>(node,ConsequenceNode.targetKeyOption, string.Empty),
                val = CreateFuzzyValue(valueType, boolVal, intVal, floatVal, stringVal),
                payLoad = GetOptionValue<string>(node, ConsequenceNode.payloadOption, string.Empty)
            };
        }

        private static RuntimeWriteBack ConvertWriteBackNode(WriteBackNode node)
        {
            FuzzyValueType valueType = GetOptionValue(node, WriteBackNode.valTypeOption, FuzzyValueType.Bool);
            bool boolVal = GetOptionValue(node, WriteBackNode.boolValOption, false);
            int intVal =GetOptionValue(node, WriteBackNode.intValOption, 0);
            float floatVal = GetOptionValue(node, WriteBackNode.floatValOption, 0f);
            string stringVal = GetOptionValue(node, WriteBackNode.stringValOption, string.Empty);

            return new RuntimeWriteBack
            {
                targetKey = GetOptionValue<string>(node, WriteBackNode.targetKeyOption, string.Empty),
                operation = GetOptionValue(node, WriteBackNode.operationOption, WriteBackOperation.Set),
                val = CreateFuzzyValue(valueType, boolVal, intVal, floatVal, stringVal)
            };
        }

        private static FuzzyValue CreateFuzzyValue(FuzzyValueType valueType, bool boolVal, int intVal, float floatVal, string stringVal)
        {
            return valueType switch
            {
                FuzzyValueType.Bool => FuzzyValue.FromBool(boolVal),
                FuzzyValueType.Int => FuzzyValue.FromInt(intVal),
                FuzzyValueType.Float => FuzzyValue.FromFloat(floatVal),
                FuzzyValueType.String => FuzzyValue.FromString(stringVal),
                _ => FuzzyValue.FromString(string.Empty)
            };
        }

        private static T GetOptionValue<T>(Node node, string optionName, T defaultValue)
        {
            if (node == null)
                return defaultValue;

            INodeOption option = node.GetNodeOptionByName(optionName);

            if (option == null)
                return defaultValue;

            if (option.TryGetValue(out T value))
                return value;

            return defaultValue;
        }

        private static IEnumerable<T> GetConnectedNodesFromOutput<T>(Node node, string outputPortName) where T : Node
        {
            IPort outputPort = node.GetOutputPortByName(outputPortName);

            return GetConnectedNodes<T>(outputPort);
        }

        private static IEnumerable<T> GetConnectedNodesFromInput<T>(Node node, string inputPortName) where T : Node
        {
            IPort inputPort = node.GetInputPortByName(inputPortName);

            return GetConnectedNodes<T>(inputPort);
        }

        private static IEnumerable<T> GetConnectedNodes<T>(IPort port) where T : Node
        {
            if (port == null)
                return Enumerable.Empty<T>();

            List<IPort> connectedPorts = new List<IPort>();

            port.GetConnectedPorts(connectedPorts);

            return connectedPorts.Select(connectedPort => connectedPort.GetNode()).OfType<T>();
        }
    }
}