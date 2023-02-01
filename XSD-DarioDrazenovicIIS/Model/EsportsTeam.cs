﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XSD_DarioDrazenovicIIS.Model
{
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/XSD-DarioDrazenovicIIS.Model")]
    public class EsportsTeamArray
    {
        //this property should be the first property in the serialized data when used with a data contract serializer.
        [DataMember(Order = 0)]
        public List<EsportsTeam> EsportsTeamList { get; set; }

        public EsportsTeamArray()
        {

        }

        public EsportsTeamArray(List<EsportsTeam> esportsTeamList)
        {
            EsportsTeamList = esportsTeamList;
        }

        [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/XSD-DarioDrazenovicIIS.Model")]
        public class EsportsTeam
        {
            public EsportsTeam(string id, string type, string name, double cost)
            {
                Id = id;
                Type = type;
                Name = name;
                Cost = cost;
            }
            public EsportsTeam()
            {

            }
            [DataMember(Order = 0)]
            public string Id { get; set; }
            [DataMember(Order = 1)]
            public string Type { get; set; }
            [DataMember(Order = 2)]
            public string Name { get; set; }
            [DataMember(Order = 3)]
            public double Cost { get; set; }
        }
    }
}