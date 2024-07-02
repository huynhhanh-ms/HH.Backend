using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Domain.Enums
{
    public static class StockCheckEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StockCheckType
        {
            Regular,
            Spot
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StockCheckStatus
        {
            Todo,
            Assigned,
            Accepted,
            AssignmentDeclined,
            Draft,
            Submitted,
            Confirmed,
            Completed,
            Rejected
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StockCheckPriority
        {
            Highest,
            //High,
            Medium
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StockCheckDetailStatus
        {
            Todo,
            Submitted,
            Confirmed,
            Rejected
        }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum StockDiff
        {
            Less,
            More,
            LessAndMore,
            Equal
        }
    }
}
