﻿using System;
using System.Linq;

namespace LangBuilder.Source.Domain
{
    public class SimpleRuleAttribute : Attribute { }

    public enum RuleType
    {
        [SimpleRule]
        DirectTranslation = 0,
        [SimpleRule]
        Expression = 1,
        Block = 2,
        RuleSequence = 3
    }

    public static class RuleTypeExtensions
    {
        public static bool IsSimple(this RuleType type)
        {
            var enumType = typeof(RuleType);
            var memberInfos = enumType.GetMember(type.ToString());
            var enumValueMemberInfo = memberInfos.FirstOrDefault(m => m.DeclaringType == enumType);
            var valueAttributes =
                enumValueMemberInfo.GetCustomAttributes(typeof(SimpleRuleAttribute), false);
            return valueAttributes.Any();
        }
    }
}