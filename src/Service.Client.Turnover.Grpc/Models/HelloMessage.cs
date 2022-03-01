using System.Runtime.Serialization;
using Service.Client.Turnover.Domain.Models;

namespace Service.Client.Turnover.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}