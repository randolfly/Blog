using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Markdig;
using Blog.Shared.Render;
using Markdig.Extensions.EmphasisExtras;
using CollectionExtensions = AngleSharp.Dom.CollectionExtensions;

namespace Blog.Server.Service;

public class RenderItemService : IRenderItemService
{
    /// <summary>
    /// Convert a markdown file to a <code>ComponentRenderItem</code>
    /// </summary>
    /// <param name="markdown"></param>
    /// <returns></returns>
    public async Task<ComponentRenderItem> ParseMarkdown(string markdown)
    {
        // convert markdown to html
        var html = ParseMarkdownToHtml(markdown);
        
        var config = Configuration.Default;
        //Create a new context for evaluating webpages with the given config
        var context = BrowsingContext.New(config);
        //Create a virtual request to specify the document to load (here from our fixed string)
        var doc = await context.OpenAsync(req => req.Content(html));

        var parseDom = ParseDom(doc.Body);
        // change the outside body element to div
        parseDom.RenderElement.Element = "div";
        return parseDom;
    }

    public string ParseMarkdownToHtml(string markdown)
    {
        // convert markdown to html
        var pipeline = new MarkdownPipelineBuilder()
            .UseDefinitionLists()
            .UseFigures()
            // .UseMathematics()
            .UseEmphasisExtras(options: EmphasisExtraOptions.Marked)
            .UseAutoLinks()
            .UseFootnotes()
            .UseListExtras()
            .UseGridTables()
            .UseMediaLinks()
            .UseTaskLists()
            .UseYamlFrontMatter()
            // .UseBootstrap()
            .Build();
        var html = Markdown.ToHtml(markdown, pipeline);
        return html;
    }

    private ComponentRenderItem ParseDom(INode htmlNode, int sequenceId = 0)
    {
        var componentNode = new ComponentRenderItem();
        // leaf node
        if (htmlNode.NodeType == NodeType.Text)
        {
            // text node name: #text
            componentNode.RenderElement = new RenderElement(sequenceId++, htmlNode.NodeName.ToLower());
            componentNode.RenderMarkupContent = new RenderMarkupContent(sequenceId++, htmlNode.TextContent);

            return componentNode;
        }
        // element node

        // attribute pairs
        List<KeyValuePair<string, object>> keyValuePairs = new();

        foreach (var attribute in ((IElement)htmlNode).Attributes)
        {
            keyValuePairs.Add(new KeyValuePair<string, object>(attribute.Name, attribute.Value));
        }

        // child elements/text node
        var childNodes = htmlNode.ChildNodes
            .Where(node => node.NodeType != NodeType.Text || node.TextContent != "\n")
            .ToList();

        componentNode.RenderElement = new RenderElement(sequenceId++, htmlNode.NodeName.ToLower());
        componentNode.RenderAttributes = new RenderAttributes(sequenceId++, keyValuePairs);

        foreach (var childElement in childNodes)
        {
            var childComponentItem = ParseDom(childElement);
            componentNode.ContentItems.Add(childComponentItem);
        }

        return componentNode;
    }
}