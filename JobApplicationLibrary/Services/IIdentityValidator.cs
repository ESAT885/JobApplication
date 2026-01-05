using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary.Services
{
    public interface IIdentityValidator
    {
        public bool IsValid(string identityNumber);
        public bool CheckConnectionToRemoteServer();

        ICountryDataProvider countryDataProvider { get; }   
    }
    public interface ICountryData
    {
        public string Country { get; }
    }
    public interface ICountryDataProvider
    {
        public ICountryData CountryData { get; }
    }
}
