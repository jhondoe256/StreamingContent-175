using StreamContent.Data.Entities;
using StreamContent.Data.Entities.Enums;
using Xunit;
namespace StreamingContent_Test;

public class StreamingContentEntityTests
{
    [Fact]
    public void SetTitle_ShouldReturnCorrectString()
    {
        //* AAA

        //* Arrange
        StreamingContentEntity content = new StreamingContentEntity();

        content.Title = "Toy Story";

        //* Action
        string expected = "Toy Story";

        string actual = content.Title;

        //* Assertion
        Assert.Equal(expected,actual);
    }

    [Theory]
    [InlineData(MaturityRating.G,true)]
    [InlineData(MaturityRating.TV_PG,true)]
    [InlineData(MaturityRating.R,false)]
    [InlineData(MaturityRating.TV_MA,false)]
    public void SetMaturityRating_ShouldGet_Correct_IsFamilyFriendly(MaturityRating maturityRating, bool expectedIsFamilyFriendly)
    {
        //* Arrange
        StreamingContentEntity content = new StreamingContentEntity("Content Title", "Some Description",4.2d,maturityRating,GenreType.Scifi);
    
        //* Action
        bool actual = content.IsFamilyFriendly;
        bool expected = expectedIsFamilyFriendly;

        Assert.Equal(expected,actual);
    }
}