﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.
// </copyright>
// <summary>
//   Assembly properties.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("EsentCollections")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Microsoft")]
[assembly: AssemblyProduct("EsentCollections")]
[assembly: AssemblyCopyright("Copyright (c) Microsoft. All Rights Reserved.")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
// 1.8.3.0 2013.03.25. Signed and Strong Named.
// 1.9.0.0 2013.12.23. Go back to targetting framework 4.0.
// 1.9.1.0 2014.07.18. PersistentDictionary gets binary blobs; added Isam layer.
[assembly: AssemblyVersion("1.9.1.0")]
[assembly: AssemblyFileVersion("1.9.1.0")]

#if STRONG_NAMED
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EsentCollectionsTests,         PublicKey=0024000004800000940000000602000000240000525341310004000001000100B5FC90E7027F67871E773A8FDE8938C81DD402BA65B9201D60593E96C492651E889CC13F1415EBB53FAC1131AE0BD333C5EE6021672D9718EA31A8AEBD0DA0072F25D87DBA6FC90FFD598ED4DA35E44C398C454307E8E33B8426143DAEC9F596836F97C8F74750E5975C64E2189F45DEF46B2A2B1247ADC3652BF5C308055DA9")]
#else
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("EsentCollectionsTests")]
#endif

