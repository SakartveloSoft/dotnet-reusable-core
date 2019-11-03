using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Metadata
{
    public class ObjectValidationReport
    {
        public string ObjectIdentity { get; set; }
        public string MetaTypeName { get; set; }

        public bool IsValid { get; set; } = true;

        public List<ValidationError> Errors { get; set; }

        public ObjectValidationReport AddError(ValidationError error)
        {
            if (Errors == null)
            {
                Errors = new List<ValidationError>();
            }
            Errors.Add(error);
            IsValid = false;
            return this;
        }


    }
}
