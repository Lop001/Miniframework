﻿using MiniFramework.Core.Metadata;
using MiniFramework.Core.Sql;
using MiniFramework.CodeGen;
using MiniFramework.TestApp;
using MiniFramework.Utils;
using System.Reflection;
using MiniFramework.Core.Reflection;
using MiniFramework.Core.CodeGen;


var assembly = Assembly.GetExecutingAssembly();
var entityTypes = EntityDiscovery.FindAllEntities(assembly);


var repoNamespace = "MyApp.Repositories";
var modelNamespace = "MiniFramework.TestApp";
var usings = new[] { modelNamespace, "MiniFramework.Core.Interfaces", "MiniFramework.Core.Base" };

// jednorázově vygeneruj BaseRepository.cs
var baseRepoCode = BaseRepositoryTemplate.Generate(repoNamespace);
CodeWriter.WriteToFile("Generated", "BaseRepository.cs", baseRepoCode);

foreach (var type in entityTypes)
{
    var metadata = EntityMetadataExtractor.Extract(type);

    

    if (!metadata.GenerateRepository)
    {
        Console.WriteLine($"✗ Přeskočeno: {metadata.Name} (GenerateRepository = false)");
        continue;
    }

    string classCode = DapperRepositoryGenerator.Generate(metadata, repoNamespace, usings, useBaseClass:true);
    string interfaceCode = DapperRepositoryGenerator.GenerateInterface(metadata, repoNamespace, usings);

    var folder = "Generated";

    CodeWriter.WriteToFile(folder, $"{metadata.Name}Repository.cs", classCode);
    CodeWriter.WriteToFile(folder, $"I{metadata.Name}Repository.cs", interfaceCode);

    Console.WriteLine($"✓ Vygenerováno: {metadata.Name}Repository.cs a I{metadata.Name}Repository.cs");
}

var diCode = DIRegistrationGenerator.Generate(
    entityTypes.Select(EntityMetadataExtractor.Extract),
    @namespace: "MyApp.DependencyInjection"
);

CodeWriter.WriteToFile("Generated", "RepositoryRegistration.cs", diCode);
Console.WriteLine("✓ Vygenerováno: RepositoryRegistration.cs");


/*

foreach (var type in entityTypes)
{
    var metadata = EntityMetadataExtractor.Extract(type);

    string classCode = DapperRepositoryGenerator.Generate(metadata);
    string interfaceCode = DapperRepositoryGenerator.GenerateInterface(metadata);

    var folder = "Generated";

    CodeWriter.WriteToFile(folder, $"{metadata.Name}Repository.cs", classCode);
    CodeWriter.WriteToFile(folder, $"I{metadata.Name}Repository.cs", interfaceCode);

    Console.WriteLine($"✓ Vygenerováno: {metadata.Name}Repository.cs a I{metadata.Name}Repository.cs");

    var sql = SqlSchemaGenerator.GenerateCreateTable(metadata);
    Console.WriteLine("\n--- SQL ---");
    Console.WriteLine(sql);
}
*/
