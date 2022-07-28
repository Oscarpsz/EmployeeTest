using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeTest.Models
{
    public class Employee
    {
        public int id { get; set; }

        public string name { get; set; }

        public string lastName { get; set; }

        [Key]
        [RegularExpression("[A-z]{4}[0-9]{6}[A-z0-9]{3}", ErrorMessage = "Is not a valid RFC")]
        public string rfc { get; set; }

        public DateTime bornDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public EmployeeStatus status { get; set; }
    }

    public enum EmployeeStatus
    {
        NotSet,
        Active,
        Inactive,
    }


}
