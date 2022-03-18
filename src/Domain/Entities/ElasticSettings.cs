using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ElasticSettings
    {

        public string PropertyIndex { get; set; }
        public string PropertyUpload { get; set; }
        public string ManagementIndex { get; set; }
        public string ManagementUpload { get; set; }
        public string URILocal { get; set; }
        public string AWTUri { get; set; }
        public string AWTAccessKeyId { get; set; }
        public string AWTSecretKey { get; set; }
        public string LocalDefaultIndex { get; set; }
        public string AWTDefaultIndex { get; set; }
        public bool UseLocal { get; set; }
        public string AWTToken { get; set; }
        public bool UseAWSBasicIAM { get; set; }
    }
}
