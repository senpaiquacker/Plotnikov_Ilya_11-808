namespace ContentSubscriptions
{
    interface IMediaContent
    {
        public string UniqueID { get; }
        public string ToMessage { get; }
    }
}
