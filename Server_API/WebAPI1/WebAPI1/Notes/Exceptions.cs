using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI1.Notes
{
    public class Exceptions
    {
        /*
         
         1.
         Type 'System.Data.Entity.DynamicProxies.Employee_59DDCE5F00ED24F9D6DAFF0463868E9215942103BF36B459ADB21C8AAEA2981F' with data contract name 'Employee_59DDCE5F00ED24F9D6DAFF0463868E9215942103BF36B459ADB21C8AAEA2981F:http://schemas.datacontract.org/2004/07/System.Data.Entity.DynamicProxies' is not expected. Consider using a DataContractResolver if you are using DataContractSerializer or add any types not known statically to the list of known types - for example, by using the KnownTypeAttribute attribute or by adding them to the list of known types passed to the serializer.

        in WebApiConfig.cs
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            
         */
    }
}