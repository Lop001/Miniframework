using System.Text;
using MiniFramework.Core.Metadata;

namespace MiniFramework.Core.Sql;

public static class SqlSchemaGenerator
{
    public static string GenerateCreateTable(EntityMetadata metadata)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"CREATE TABLE [{metadata.TableName}] (");

        var fields = metadata.Fields;

        for (int i = 0; i < fields.Count; i++)
        {
            var field = fields[i];
            string line = $"    [{field.Name}] {MapType(field.Type)}";

            if (field.IsPrimaryKey)
                line += " PRIMARY KEY IDENTITY(1,1)";
            else if (field.Required)
                line += " NOT NULL";
            else
                line += " NULL";

            if (i < fields.Count - 1 || fields.Any(f => f.IsForeignKey))
            line += ",";

            sb.AppendLine(line);
        }

        // Cizí klíče
        foreach (var fk in fields.Where(f => f.IsForeignKey))
        {
            sb.AppendLine($"    FOREIGN KEY ([{fk.Name}]) REFERENCES [{fk.Relation!.TargetEntity}]([{fk.Relation.TargetKey}])" +
                        (fk == fields.Last(f => f.IsForeignKey) ? "" : ","));
        }

        sb.AppendLine(");");
        return sb.ToString();
    }

    private static string MapType(string type)
    {
        return type switch
        {
            "Int32" => "INT",
            "String" => "NVARCHAR(255)",
            "DateTime" => "DATETIME",
            "Boolean" => "BIT",
            "Double" => "FLOAT",
            "Decimal" => "DECIMAL(18,2)",
            _ => "NVARCHAR(MAX)" // fallback
        };
    }
}
