using System;

[Serializable]
public abstract class Reference<T>
{
    public bool UseConstant = true;
    public T ConstantValue;
    public ReferenceVariable<T> Variable;

    public T Value => UseConstant ? ConstantValue : Variable.Value;
}