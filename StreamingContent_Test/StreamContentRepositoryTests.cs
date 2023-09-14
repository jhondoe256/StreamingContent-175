using StreamContent.Repository;
using StreamContent.Data.Entities;
using StreamContent.Data.Entities.Enums;

namespace StreamingContent_Test;

public class StreamContentRepositoryTests
{
    private StreamContentRepository _globalRepo;

    private StreamingContentEntity _contentA;
    private StreamingContentEntity _contentB;
    private StreamingContentEntity _contentC;
    private StreamingContentEntity _contentD;

    public StreamContentRepositoryTests()
    {
        _globalRepo = new StreamContentRepository();

        _contentA = new StreamingContentEntity("Rubber","A tire that kills people",2,MaturityRating.R,GenreType.Horror);
        _contentB = new StreamingContentEntity("Bad Boys","A Cop Movie",10, MaturityRating.R,GenreType.Action);
        _contentC = new StreamingContentEntity("The Last Action Hero","Another Action Movie",10,MaturityRating.R,GenreType.Action);
        _contentD = new StreamingContentEntity("50 First Dates","Romance Flick, Sandler's Awesome!",10,MaturityRating.PG_13,GenreType.RomCom);
       
       //* Add the data to the Repository
       _globalRepo.AddContentToDb(_contentA);
       _globalRepo.AddContentToDb(_contentB);
       _globalRepo.AddContentToDb(_contentC);
       _globalRepo.AddContentToDb(_contentD);
    }

    [Fact]
    public void AddToDirectory_ShouldGetCorrectBoolean()
    {
        //* AAA

        //* Arrange 
        StreamingContentEntity content = new StreamingContentEntity();

        //* Action
        bool addResult = _globalRepo.AddContentToDb(content);

        //* Assert
        Assert.True(addResult);

    }

    [Fact]
    public void Get_Database_Info_ShouldReturn_CorrectCollection()
    {
        //* Act 
        //* there should be 4 total stContentEntities 
        int expectedCount = 4;
        int actualCount = _globalRepo.GetAllStreamingContent().Count;

        //* Assert
        Assert.Equal(expectedCount,actualCount);        
    }

    [Fact]
    public void GetTitle_Should_Return_Correct_Content()
    {
        //* Arrange... already done

        //* Act
        StreamingContentEntity searchResult = _globalRepo.GetStreamingContentByTitle("Rubber");

        //* Assert 
        Assert.Equal(searchResult,_contentA);
    }

    [Fact]
    public void UpdateExistingContent_Should_Return_True()
    {
        //* Arrange
        //* Think of these values as coming form an updated form
        StreamingContentEntity updatedContent = new StreamingContentEntity(
                                                                            "Rubber UPDATED",
                                                                            "You know what it does...KILLS!",
                                                                            7.0,
                                                                            MaturityRating.PG,
                                                                            GenreType.Horror);

        bool updatedResult = _globalRepo.UpdateExistingContent("Rubber",updatedContent);

        Assert.True(updatedResult);                                                            
    }

    [Fact]
    public void Delete_Existing_Content_Should_Return_True()
    {
        //* Arrange
        StreamingContentEntity content = _globalRepo.GetStreamingContentByTitle("Rubber");

        //* Act
        bool removeResult = _globalRepo.DeleteExistingContent(content);
        int expectedCount =3;
        int actualCount = _globalRepo.GetAllStreamingContent().Count;
        
        //* Assert
        Assert.True(removeResult);
        Assert.Equal(expectedCount,actualCount);
    }

    [Fact]
    public void GetContentByMaturityRating()
    {
        var contents = _globalRepo.GetContentByMaturityRating(MaturityRating.PG_13);
        Assert.Equal(1,contents.Count);
    }
}