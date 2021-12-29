using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Rhetos.Dsl;
using Rhetos.Dsl.DefaultConcepts;



namespace Rhetos.Dsl.DefaultConcepts
{
    /// <summary>
    /// Automatically enters time when the records was created.
    /// </summary>
    [Export(typeof(IConceptInfo))]
    [ConceptKeyword("LastModifiedTime")]
    public class LastModifiedTimeInfo : IConceptInfo
    {
        [ConceptKey]
        public DateTimePropertyInfo Property { get; set; }
    }
}

