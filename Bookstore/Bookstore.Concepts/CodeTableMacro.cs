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
    public class CodeTableMacro : IConceptMacro<CodeTableInfo>
    {
        public IEnumerable<IConceptInfo> CreateNewConcepts(CodeTableInfo conceptInfo, IDslModel existingConcepts)
        {
            var newConcepts = new List<IConceptInfo>();

            ShortStringPropertyInfo code = new ShortStringPropertyInfo{ DataStructure = conceptInfo.Entity, Name = "Code" };
            newConcepts.Add(code);
            newConcepts.Add(new AutoCodePropertyInfo  
            {
                Property = code

            });

            ShortStringPropertyInfo name = new ShortStringPropertyInfo { DataStructure = conceptInfo.Entity, Name = "Name" };
            newConcepts.Add(name);
            newConcepts.Add(new RequiredPropertyInfo
            {
                Property = name
            });

            return newConcepts;
        }
    }
}