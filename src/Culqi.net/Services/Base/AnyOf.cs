using Culqi.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Culqi.Services.Base
{
    public abstract class AnyOf : IAnyOf
    {
        public abstract object Value { get; }
        public abstract Type Type { get; }
        public override string ToString() => this.Value == null ? "AnyOf(null)" : this.Value.ToString();
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic variant")]
    public class AnyOf<T1, T2> : AnyOf
    {
        private readonly T1 value1;
        private readonly T2 value2;
        private readonly Values setValue;

        public AnyOf(T1 value)
        {
            this.value1 = value;
            this.setValue = Values.Value1;
        }

        public AnyOf(T2 value)
        {
            this.value2 = value;
            this.setValue = Values.Value2;
        }

        private enum Values
        {
            Value1,
            Value2,
        }

        public override object Value
        {
            get
            {
                switch (this.setValue)
                {
                    case Values.Value1:
                        return this.value1;
                    case Values.Value2:
                        return this.value2;
                    default:
                        throw new InvalidOperationException($"Unexpected state, setValue={this.setValue}");
                }
            }
        }

        public override Type Type
        {
            get
            {
                switch (this.setValue)
                {
                    case Values.Value1:
                        return typeof(T1);
                    case Values.Value2:
                        return typeof(T2);
                    default:
                        throw new InvalidOperationException($"Unexpected state, setValue={this.setValue}");
                }
            }
        }

        public static implicit operator AnyOf<T1, T2>(T1 value) => value == null ? null : new AnyOf<T1, T2>(value);
        public static implicit operator AnyOf<T1, T2>(T2 value) => value == null ? null : new AnyOf<T1, T2>(value);
        public static implicit operator T1(AnyOf<T1, T2> anyOf) => anyOf.value1;
        public static implicit operator T2(AnyOf<T1, T2> anyOf) => anyOf.value2;
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleType", Justification = "Generic variant")]
    public class AnyOf<T1, T2, T3> : AnyOf
    {
        private readonly T1 value1;
        private readonly T2 value2;
        private readonly T3 value3;
        private readonly Values setValue;

        public AnyOf(T1 value)
        {
            this.value1 = value;
            this.setValue = Values.Value1;
        }

        public AnyOf(T2 value)
        {
            this.value2 = value;
            this.setValue = Values.Value2;
        }

        public AnyOf(T3 value)
        {
            this.value3 = value;
            this.setValue = Values.Value3;
        }

        private enum Values
        {
            Value1,
            Value2,
            Value3,
        }

        public override object Value
        {
            get
            {
                switch (this.setValue)
                {
                    case Values.Value1:
                        return this.value1;
                    case Values.Value2:
                        return this.value2;
                    case Values.Value3:
                        return this.value3;
                    default:
                        throw new InvalidOperationException($"Unexpected state, setValue={this.setValue}");
                }
            }
        }

        public override Type Type
        {
            get
            {
                switch (this.setValue)
                {
                    case Values.Value1:
                        return typeof(T1);
                    case Values.Value2:
                        return typeof(T2);
                    case Values.Value3:
                        return typeof(T3);
                    default:
                        throw new InvalidOperationException($"Unexpected state, setValue={this.setValue}");
                }
            }
        }

        public static implicit operator AnyOf<T1, T2, T3>(T1 value) => value == null ? null : new AnyOf<T1, T2, T3>(value);
        public static implicit operator AnyOf<T1, T2, T3>(T2 value) => value == null ? null : new AnyOf<T1, T2, T3>(value);
        public static implicit operator AnyOf<T1, T2, T3>(T3 value) => value == null ? null : new AnyOf<T1, T2, T3>(value);
        public static implicit operator T1(AnyOf<T1, T2, T3> anyOf) => anyOf.value1;
        public static implicit operator T2(AnyOf<T1, T2, T3> anyOf) => anyOf.value2;
        public static implicit operator T3(AnyOf<T1, T2, T3> anyOf) => anyOf.value3;
    }
}
