namespace NGrid.Customer.ToReplace.Api
{
    public static class AppConstants
    {
        public const string HealthEndPoint = "/health";
        public const string Internal = "internal";
        public const string Graphql = "graphql";

        public const string ToReplaceApiName = "ToReplace Bounded Context API";
        public const string ToReplaceApiPath = "toReplace-api";
        public const string AppVersion = "v1";
        public const string ApiInternalPath = "/internal";
        public const string ToReplaceApiPublicGroupName = ToReplaceApiPath + "-" + AppVersion;
        public const string ToReplaceApiInternalGroupName = ToReplaceApiPath + "-" + Internal;

        internal const string Source = "API";

        public const string ToReplaceLookupFeatureName = "ToReplace Lookup";
        public const string DataLoadFeatureName = "Data Load";
        public const string FeatureIsUnavailableErrorMessageToReplace = "{0} feature is unavailable";
    }
}
