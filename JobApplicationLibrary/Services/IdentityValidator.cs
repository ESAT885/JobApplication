using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary.Services
{
    public class IdentityValidator: IIdentityValidator
    {
        public ICountryDataProvider countryDataProvider => throw new NotImplementedException();

        public ValidationMode ValidationMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CheckConnectionToRemoteServer()
        {
            return true;
        }

        public bool IsValid(string identityNumber)
        {
            return true;
        }
    }
}
