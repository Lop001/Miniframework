using System.Text;
using MiniFramework.Core.Metadata;

namespace MiniFramework.Core.CodeGen;

public static class ApiControllerGenerator
{
    public static string Generate(EntityMetadata meta, string @namespace = "MyApp.Api", string[]? extraUsings = null)
    {
        var sb = new StringBuilder();
        string entityName = meta.Name;
        string controllerName = entityName + "Controller";
        string interfaceName = $"I{entityName}Repository";
        string route = meta.TableName.ToLower();

        if (extraUsings != null)
        {
            foreach (var u in extraUsings)
                sb.AppendLine($"using {u};");
        }

        sb.AppendLine("using Microsoft.AspNetCore.Mvc;");
        sb.AppendLine("using System.Threading.Tasks;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine();
        sb.AppendLine($"namespace {@namespace}");
        sb.AppendLine("{");
        sb.AppendLine("    [ApiController]");
        sb.AppendLine($"    [Route(\"api/{route}\")]");
        sb.AppendLine($"    public partial class {controllerName} : ControllerBase");
        sb.AppendLine("    {");
        sb.AppendLine($"        private readonly {interfaceName} _repository;");
        sb.AppendLine();
        sb.AppendLine($"        public {controllerName}({interfaceName} repository)");
        sb.AppendLine("        {");
        sb.AppendLine("            _repository = repository;");
        sb.AppendLine("        }");
        sb.AppendLine();

        sb.AppendLine("        [HttpGet]");
        sb.AppendLine($"        public async Task<IEnumerable<{entityName}>> GetAll() => await _repository.GetAllAsync();");
        sb.AppendLine();

        sb.AppendLine("        [HttpGet(\"{id}\")]");
        sb.AppendLine($"        public async Task<ActionResult<{entityName}>> Get(int id)");
        sb.AppendLine("        {");
        sb.AppendLine("            var item = await _repository.GetByIdAsync(id);");
        sb.AppendLine("            return item is null ? NotFound() : Ok(item);");
        sb.AppendLine("        }");
        sb.AppendLine();

        sb.AppendLine("        [HttpPost]");
        sb.AppendLine($"        public async Task<IActionResult> Create({entityName} entity)");
        sb.AppendLine("        {");
        sb.AppendLine("            await _repository.InsertAsync(entity);");
        sb.AppendLine("            return Ok();");
        sb.AppendLine("        }");
        sb.AppendLine();

        sb.AppendLine("        [HttpPut(\"{id}\")]");
        sb.AppendLine($"        public async Task<IActionResult> Update(int id, {entityName} entity)");
        sb.AppendLine("        {");
        sb.AppendLine("            entity.Id = id; // přiřazení PK");
        sb.AppendLine("            await _repository.UpdateAsync(entity);");
        sb.AppendLine("            return Ok();");
        sb.AppendLine("        }");
        sb.AppendLine();

        sb.AppendLine("        [HttpDelete(\"{id}\")]");
        sb.AppendLine("        public async Task<IActionResult> Delete(int id)");
        sb.AppendLine("        {");
        sb.AppendLine("            await _repository.DeleteAsync(id);");
        sb.AppendLine("            return Ok();");
        sb.AppendLine("        }");

        sb.AppendLine("    }");
        sb.AppendLine("}");

        return sb.ToString();
    }
}
