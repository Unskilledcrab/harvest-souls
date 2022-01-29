public abstract class Reference<T, TVariable>
    where TVariable : ReferenceVariable<T>
{
    public bool UseConstant = true;
    public T ConstantValue;
    public TVariable Variable;

    public T Value => UseConstant ? ConstantValue : Variable.Value;
}