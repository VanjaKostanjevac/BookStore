using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Concepts
{


    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("CodeTable")]

    public class CodeTableInfo : IConceptInfo
    {
        [ConceptKey]
        public EntityInfo Entity { get; set; }



    }
}