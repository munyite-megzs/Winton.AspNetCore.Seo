using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace Winton.AspNetCore.Seo.HeaderMetadata.OpenGraph.Videos
{
    public class OpenGraphVideoMovieTagHelperTests
    {
        public sealed class Process : OpenGraphVideoMovieTagHelperTests
        {
            private static readonly MetaTag NamespaceMetaTag = new MetaTag(
                "OpenGraphNamespaceTagHelperComponent",
                "og: http://ogp.me/ns# video: http://ogp.me/ns/video#");

            private static readonly MetaTag TypeMetaTag = new MetaTag("og:type", "video.movie");

            public static IEnumerable<object[]> TestCases => new List<object[]>
            {
                new object[]
                {
                    new OpenGraphVideoMovieTagHelper(),
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
                OpenGraphVideoMovieTagHelper tagHelper,
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

        public sealed class Type : OpenGraphVideoMovieTagHelperTests
        {
            [Fact]
            private void ShouldBeVideoMovie()
            {
                var tagHelper = new OpenGraphVideoMovieTagHelper();

                string? type = tagHelper.Type;

                type.Should().Be("video.movie");
            }
        }
    }
}