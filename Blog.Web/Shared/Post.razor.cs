using System.Net.Http.Json;
using Console = System.Console;

namespace Blog.Web.Shared;

using Blog.Shared.Render;
using Microsoft.AspNetCore.Components;

public partial class Post
{
    private RenderFragment Body { get; set; }
    private ComponentRenderItem? ComponentRenderTree { get; set; }

    [Inject] public HttpClient HttpClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await RenderComponent();
    }

    private RenderFragment CreatePost(ComponentRenderItem renderItem) => builder =>
    {
        builder.OpenElement(renderItem.RenderElement.SequenceId, renderItem.RenderElement.Element);

        // NOTE the result here shows that the string-object key map is not valid, it requires cast
        // just switch back to Dictionary and use AddAttribute
        if (renderItem.RenderAttributes != null)
        {
            builder.AddMultipleAttributes(renderItem.RenderAttributes.SequenceId,
                renderItem.RenderAttributes.Attributes);
        }

        if (renderItem.ContentItems.Count != 0)
        {
            foreach (var contentItem in renderItem.ContentItems)
            {
                var contentRenderFragment = CreatePost(contentItem);
                builder.AddContent(contentItem.RenderElement.SequenceId, contentRenderFragment);
            }
        }

        // render the text content node last
        if (renderItem.RenderMarkupContent != null)
        {
            builder.AddMarkupContent(renderItem.RenderMarkupContent.SequenceId,
                renderItem.RenderMarkupContent.MarkupContent);
        }

        builder.CloseElement();

        // self defined component
        // builder.OpenComponent(3, typeof(TestComponent));
        // builder.AddAttribute(4, "Msg", "hhhh");
        // builder.CloseComponent();
    };

    //parse-markdown-to-dom?markdownFilePath=E%3A%5CCode%5CC%23%5CTool%5CBlazorBlog%5CBlazorBlog.Server%5Cassets%5Chello.md
    private async Task RenderComponent()
    {
        var mdFilePath = @"E:\Code\C#\Tool\BlazorBlog\BlazorBlog.Server\assets\傅里叶变换.md";
        var uri = System.Web.HttpUtility.UrlEncode(mdFilePath);
        var renderItem =
            await HttpClient.GetFromJsonAsync<ComponentRenderItem>($"parse-markdown-to-dom?markdownFilePath={uri}");
        Console.WriteLine("Initial Render Tree Successfully");

        Body = CreatePost(renderItem);
        StateHasChanged();
    }
}