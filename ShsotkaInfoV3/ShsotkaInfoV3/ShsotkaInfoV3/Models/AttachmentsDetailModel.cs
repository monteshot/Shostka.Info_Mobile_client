using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace ShsotkaInfoV3.Services
{
    [Serializable]
    public class AttachmentsDetailModel //featured
    {
        [JsonProperty("source_url")]
        public string SourceURL { get; set; }
    }
    

}