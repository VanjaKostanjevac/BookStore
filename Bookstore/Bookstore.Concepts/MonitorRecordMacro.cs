using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;

namespace Bookstore.Concepts
{
    [Export(typeof(IConceptMacro))]
    public class MonitorRecordMacro: IConceptMacro<MonitorRecordInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(MonitorRecordInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            DateTimePropertyInfo createdAt = new DateTimePropertyInfo { DataStructure = conceptInfo.Entity, Name = "CreatedAt" };

            newConcepts.Add(createdAt);

            newConcepts.Add(new CreationTimeInfo { Property = createdAt });
            newConcepts.Add(new DenyUserEditPropertyInfo { Property = createdAt});


            


            return newConcepts;
        }
    }
}