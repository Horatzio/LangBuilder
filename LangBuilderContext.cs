using Microsoft.EntityFrameworkCore;

namespace LangBuilder
{
    public class LangBuilderContext : DbContext
    {
        public LangBuilderContext(DbContextOptions<LangBuilderContext> options) : base(options)
        { }
    }
}
