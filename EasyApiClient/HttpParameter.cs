namespace EasyApiClient
{
    public class HttpParameter
    {
        public HttpParameter(string name)
        {
            _name = name;
        }


        private string _name;
        public string Name
        {
            get
            {
                return AliasName ?? _name;
            }
            set
            {
                _name = value;
            }
        }
        public string AliasName { get; set; }

        public bool IsFormBody { get; set; }
        public bool IsQueryString { get; set; }
        public bool IsRoute { get; set; }

        public int ParamIndex { get; set; }
    }
}
