//======
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//======
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//======
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//======
using System;
using System.Runtime.Serialization;

namespace Microsoft.Practices.Prism.Modularity
{
    /// <summary>
    /// Exception thrown when a requested <see cref="ModuleInfo"/> is not found.
    /// </summary>
    [Serializable]
    public partial class ModuleNotFoundException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleNotFoundException"/> class
        /// with the serialization data.
        /// </summary>
        /// <param name="info">Holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">Contains contextual information about the source or destination.</param>
        protected ModuleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
