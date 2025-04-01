namespace MiniFramework.CodeGen;

public static class IBaseRepositoryTemplate
{
    public static string Generate(string @namespace = "Generated.Repositories")
    {
        return $@"using System.Collections.Generic;
using System.Threading.Tasks;

namespace {@namespace}
{{
    public interface IBaseRepository<TEntity>
    {{
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
    }}
}}";
    }
}