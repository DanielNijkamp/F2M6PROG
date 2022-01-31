using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace F2M6PROG
{
    class User
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("SecurityClearance")]
        public int SecurityClearance { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
        
    }
}
