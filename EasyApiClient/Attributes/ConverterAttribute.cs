namespace EasyApiClient.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ConverterAttribute : Attribute
    {
        public ConverterAttribute(Type converterType)
        {
            ConverterType = converterType;
        }
        public Type ConverterType { get; }
    }
}
