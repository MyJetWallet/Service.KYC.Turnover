using System.Runtime.Serialization;
using Service.KYC.Turnover.Domain.Models;

namespace Service.KYC.Turnover.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}