using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos
{
    public class OpenGraphVideoTvShowTagHelperTests
    {
        public sealed class Process : OpenGraphVideoTvShowTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "og: http://ogp.me/ns# video: http://ogp.me/ns/video#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "video.tv_show");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphVideoTvShowTagHelper(),
                    new List<MetaTag>
                    {
                        TypeMetaTag,
                        NamespaceMetaTag
                    }
                }
            };

            [Theory]
            [MemberData(nameof(TestCases))]
            private void ShouldContainCorrectMetaTags(
                OpenGraphVideoTvShowTagHelper tagHelper,
                IEnumerable<MetaTag> expectedMetaTags)
            {
                TagHelperContext context = TagHelperTestUtils.CreateDefaultContext();
                TagHelperOutput output = TagHelperTestUtils.CreateDefaultOutput();

                tagHelper.Process(context, output);

                output
                    .Should()
                    .HaveMetaTagsEquivalentTo(expectedMetaTags, options => options.WithStrictOrdering());
            }
        }

        public sealed class Type : OpenGraphVideoTvShowTagHelperTests
        {
            [Fact]
            private void ShouldBeVideo()
            {
                var tagHelper = new OpenGraphVideoTvShowTagHelper();

                string? type = tagHelper.Type;

                type.Should().Be("video.tv_show");
            }
        }
    }
}