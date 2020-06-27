using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using DCMAPI.Common;

namespace DCMAPI.Arguments
{
    [DataContract]
    public class UserLoginInputArgs
    {
        [DataMember]
        
        [Required(ErrorMessage = "User name is required."), MaxLength(100, ErrorMessage = "User name can not exceed 100 characters.")]
        public string UserName { get; set; }

        [DataMember]
        [Required(ErrorMessage = "Password is required.")]
        public string UserPassword { get; set; }

        [DataMember]
        public bool IsToken { get; set; }
    }
}