namespace BlazorApp5.Components.Models
{
    public class TestCaseData
    {
        public class Root
        {
            public List<PortCall>? PortCalls { get; set; }
            public string? Code { get; set; }
        }

        public class PortCall
        {
            public int Sequence { get; set; }
            public string? Name { get; set; }
            public string? Function { get; set; }
            public List<Activity>? Activities { get; set; }
        }

        public class Activity
        {
            public string? VoyageNumber { get; set; }
            public string? Function { get; set; }
            public string? Name { get; set; }
            public string? Time { get; set; }
        }
    }

}
