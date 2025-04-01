using System.Text;
using MiniFramework.Core.Attributes;
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

        if (meta.AuthorizeAttributes.Any())
        {
            foreach (var auth in meta.AuthorizeAttributes)
            {
                if (auth.Role != null)
                    sb.AppendLine($"    [Authorize(Roles = \"{auth.Role}\")]");
                else if (auth.Policy != null)
                    sb.AppendLine($"    [Authorize(Policy = \"{auth.Policy}\")]");
                else
                    sb.AppendLine("    [Authorize]");
            }
    }
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

        var getAllAuth = GetAuthorizeLine(meta, CrudAction.GetAll);
        if (getAllAuth != null) sb.AppendLine($"        {getAllAuth}");

        sb.AppendLine("        [HttpGet]");
        sb.AppendLine($"        public async Task<IEnumerable<{entityName}>> GetAll() => await _repository.GetAllAsync();");
        sb.AppendLine();

        var getByIdAuth = GetAuthorizeLine(meta, CrudAction.GetById);
        if (getByIdAuth != null) sb.AppendLine($"        {getByIdAuth}");

        sb.AppendLine("        [HttpGet(\"{id}\")]");
        sb.AppendLine($"        public async Task<ActionResult<{entityName}>> Get(int id)");
        sb.AppendLine("        {");
        sb.AppendLine("            var item = await _repository.GetByIdAsync(id);");
        sb.AppendLine("            return item is null ? NotFound() : Ok(item);");
        sb.AppendLine("        }");
        sb.AppendLine();

        var createAuth = GetAuthorizeLine(meta, CrudAction.Create);
        if (createAuth != null) sb.AppendLine($"        {createAuth}");

        sb.AppendLine("        [HttpPost]");
        sb.AppendLine($"        public async Task<IActionResult> Create({entityName} entity)");
        sb.AppendLine("        {");
        sb.AppendLine("            await _repository.InsertAsync(entity);");
        sb.AppendLine("            return Ok();");
        sb.AppendLine("        }");
        sb.AppendLine();

        var updateAuth = GetAuthorizeLine(meta, CrudAction.Update);
        if (updateAuth != null) sb.AppendLine($"        {updateAuth}");

        sb.AppendLine("        [HttpPut(\"{id}\")]");
        sb.AppendLine($"        public async Task<IActionResult> Update(int id, {entityName} entity)");
        sb.AppendLine("        {");
        sb.AppendLine("            entity.Id = id; // přiřazení PK");
        sb.AppendLine("            await _repository.UpdateAsync(entity);");
        sb.AppendLine("            return Ok();");
        sb.AppendLine("        }");
        sb.AppendLine();

        var deleteAuth = GetAuthorizeLine(meta, CrudAction.Delete);
        if (deleteAuth != null) sb.AppendLine($"        {deleteAuth}");

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

    private static string? GetAuthorizeLine(EntityMetadata meta, CrudAction action)
    {
        var auth = meta.ActionAuthorizations.FirstOrDefault(a => a.Action == action);
        if (auth == null) return null;

        if (!string.IsNullOrWhiteSpace(auth.Policy))
            return $"[Authorize(Policy = \"{auth.Policy}\")]";

        if (auth.Roles.Length == 0)
            return "[Authorize]";

        var rolesJoined = string.Join(",", auth.Roles);
        return $"[Authorize(Roles = \"{rolesJoined}\")]";
    }
}
