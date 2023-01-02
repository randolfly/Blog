using Blog.Shared.Render;

namespace Blog.Server.Service;

public interface IRenderItemService
{
    public Task<ComponentRenderItem> ParseMarkdown(string markdown);
    public string ParseMarkdownToHtml(string markdown);
}