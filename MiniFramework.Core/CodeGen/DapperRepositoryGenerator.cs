using System.Text;
using MiniFramework.Core.Metadata;

namespace MiniFramework.CodeGen;

public static class DapperRepositoryGenerator
{
    public static string GenerateInterface(EntityMetadata meta,  string @namespace = "Generated.Repositories", string[]? extraUsings = null)
    {
        var sb = new StringBuilder();

        string entityName = meta.Name;
        string tableName = meta.TableName;
        string pkType = meta.PrimaryKey.PropertyType.Name; //    Property.PropertyType.Name;
        string pkName = meta.PrimaryKey.Name;

        string interfaceName = $"I{entityName}Repository";
   
        if (extraUsings != null)
        {
            foreach (var u in extraUsings)
                sb.AppendLine($"using {u};");
        }
        sb.AppendLine("using System.Data;");
        sb.AppendLine();
        sb.AppendLine($"namespace {@namespace}");
        sb.AppendLine("{");
        sb.AppendLine($"public interface {interfaceName}");
        sb.AppendLine("{");
        sb.AppendLine($"    Task<IEnumerable<{entityName}>> GetAllAsync();");
        sb.AppendLine($"    Task<{entityName}?> GetByIdAsync({pkType} id);");
        sb.AppendLine($"    Task InsertAsync({entityName} entity);");
        sb.AppendLine($"    Task UpdateAsync({entityName} entity);");
        sb.AppendLine($"    Task DeleteAsync({pkType} id);");
        sb.AppendLine("}");
        sb.AppendLine("}");

        return sb.ToString();
    }

    public static string Generate<T>() => Generate(EntityMetadataExtractor.Extract<T>());


    public static string Generate(EntityMetadata meta, string @namespace = "Generated.Repositories", string[]? extraUsings = null, bool useBaseClass = true)
    {
        var sb = new StringBuilder();

        if (extraUsings != null)
        {
            foreach (var u in extraUsings)
                sb.AppendLine($"using {u};");
        }
        sb.AppendLine("using System.Data;");
        sb.AppendLine("using Dapper;");
        sb.AppendLine();
        sb.AppendLine($"namespace {@namespace}");
        sb.AppendLine("{");

        string className = meta.Name + "Repository";
        string entityName = meta.Name;
        string tableName = meta.TableName;
        string pkName = meta.PrimaryKey.Name;

        sb.AppendLine();
        sb.AppendLine($"\tpublic partial class {className} : BaseRepository<{entityName}>, I{entityName}Repository");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tprivate readonly IDbConnection _connection;");
        sb.AppendLine();

        sb.AppendLine($"\t\tpublic {className}(IDbConnection connection) : base(connection)");
        sb.AppendLine("\t\t{");
        sb.AppendLine("\t\t}");



        sb.AppendLine();

        if (!useBaseClass)
        {
        // GetAll
            sb.AppendLine($"\t\tpublic async Task<IEnumerable<{entityName}>> GetAllAsync() =>");
            sb.AppendLine($"\t\t\tawait _connection.QueryAsync<{entityName}>(\"SELECT * FROM [{tableName}]\");");
            sb.AppendLine();

            // GetById
            sb.AppendLine($"\t\tpublic async Task<{entityName}?> GetByIdAsync(int id) =>");
            sb.AppendLine($"\t\t\tawait _connection.QueryFirstOrDefaultAsync<{entityName}>(\"SELECT * FROM [{tableName}] WHERE [{pkName}] = @Id\", new {{ Id = id }});");
        }
        
        sb.AppendLine();

        // Insert
        var insertFields = meta.Fields.Where(f => !f.IsPrimaryKey).ToList();
        string insertCols = string.Join(", ", insertFields.Select(f => $"[{f.Name}]"));
        string insertParams = string.Join(", ", insertFields.Select(f => "@" + f.Name));

        sb.AppendLine($"\t\tpublic async Task InsertAsync({entityName} entity) =>");
        sb.AppendLine($"\t\t\tawait _connection.ExecuteAsync(\"INSERT INTO [{tableName}] ({insertCols}) VALUES ({insertParams})\", entity);");
        sb.AppendLine();

        // Update
        string updateSet = string.Join(", ", insertFields.Select(f => $"[{f.Name}] = @{f.Name}"));

        sb.AppendLine($"\t\tpublic async Task UpdateAsync({entityName} entity) =>");
        sb.AppendLine($"\t\t\tawait _connection.ExecuteAsync(\"UPDATE [{tableName}] SET {updateSet} WHERE [{pkName}] = @{pkName}\", entity);");
        sb.AppendLine();

        // Delete
        sb.AppendLine($"\t\tpublic async Task DeleteAsync(int id) =>");
        sb.AppendLine($"\t\t\tawait _connection.ExecuteAsync(\"DELETE FROM [{tableName}] WHERE [{pkName}] = @Id\", new {{ Id = id }});");

        sb.AppendLine("\t}");
        sb.AppendLine("}");

        return sb.ToString();
    }
}
