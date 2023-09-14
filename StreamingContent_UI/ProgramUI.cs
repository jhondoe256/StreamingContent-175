

using System.Globalization;
using StreamContent.Data.Entities;
using StreamContent.Data.Entities.Content;
using StreamContent.Data.Entities.Enums;
using StreamContent.Repository;

public class ProgramUI
{
    //* access our 'back-end' repository
    private readonly StreamContentRepository _repo = new StreamContentRepository();

    public ProgramUI() { }

    public void Run()
    {
        SeedContentList();
        RunApplication();
    }

    private void RunApplication()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine("Enter the Number of the option you would like to select:\n" +
                              "1. Show All Streaming Content\n" +
                              "2. Find Streaming Content By Title\n" +
                              "3. Add New Streaming Content\n" +
                              "4. Remove Streaming Content\n" +
                              "5. Update Existing Content\n" +
                              "6. Exit\n" +
                              "============================================\n");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    ShowAllContent();
                    break;
                case "2":
                    ShowContentByTitle();
                    break;
                case "3":
                    CreateNewContent();
                    break;
                case "4":
                    RemoveContentFromList();
                    break;
                    case "5":
                    UpdateExistingContent();
                    break;
                case "6":
                    isRunning = CloseApplication();
                    break;
                default:
                    System.Console.WriteLine("Sorry, Invalid selection, please try again.");
                    break;
            }
        }
    }

    private void UpdateExistingContent()
    {
        Console.Clear();
         Console.WriteLine("Enter a Title:");

        string userInput = Console.ReadLine()!;

        StreamingContentEntity content = _repo.GetStreamingContentByTitle(userInput);

        //* defensive coding
        if (content != null)
        {
           StreamingContentEntity updatedData = AddStreamingContentDetails();
           if(_repo.UpdateExistingContent(content.Title,updatedData))
           {
                Console.WriteLine("Success");
           }
           else
           {
              Console.WriteLine("Failure");
           }
        }
        else
        {
            Console.WriteLine("Invalid Title. Could not find results.");
        }

        PressAnyKey();
    }


    private void ShowAllContent()
    {
        Console.Clear();
        List<StreamingContentEntity> listOfContent = _repo.GetAllStreamingContent();

        //* Defensive coding
        if (listOfContent.Count > 0)
        {
            foreach (StreamingContentEntity content in listOfContent)
            {
                ShowStreamingContentDetails(content);
            }
            PressAnyKey();
        }
        else
        {
            System.Console.WriteLine("Sorry, there is no Available Content!");
        }
    }

    //* Helper Method....
    private void ShowStreamingContentDetails(StreamingContentEntity content)
    {
        System.Console.WriteLine($"Title: {content.Title}\n" +
                                 $"Description: {content.Description}\n" +
                                 $"Star Rating: {content.StarRating}\n" +
                                 $"Matruity Rating: {content.MaturityRating}\n" +
                                 $"Genre: {content.GenreType}\n" +
                                 "-------------------------------------------------\n");
    }

    private void ShowContentByTitle()
    {
        Console.Clear();

        Console.WriteLine("Enter a Title");

        string userInput = Console.ReadLine()!;

        StreamingContentEntity content = _repo.GetStreamingContentByTitle(userInput);

        //* defensive coding
        if (content != null)
        {
            ShowStreamingContentDetails(content);
        }
        else
        {
            Console.WriteLine("Invalid Title. Could not find results.");
        }

        PressAnyKey();
    }

    private void CreateNewContent()
    {
        Console.Clear();

        StreamingContentEntity content = AddStreamingContentDetails();

        if (_repo.AddContentToDb(content))
        {
            Console.WriteLine("Added To the Database.");
        }
        else
        {
            Console.WriteLine("Failed to add Content to the Database.");
        }

        PressAnyKey();
    }

    private StreamingContentEntity AddStreamingContentDetails()
    {
        Console.Clear();

        StreamingContentEntity content = new StreamingContentEntity();

        //* Title
        Console.WriteLine("Please input a Title:");
        string userInputTitle = Console.ReadLine()!;
        content.Title = userInputTitle;

        //*Description
        Console.WriteLine("Please input a Description:");
        string userInputDescription = Console.ReadLine()!;
        content.Description = userInputDescription;

        //* starRating
        Console.WriteLine("Please input a Star Rating:");
        string userInputStarRating = Console.ReadLine()!;
        content.StarRating = Convert.ToDouble(userInputStarRating);

        //* Maturity Rating
        Console.WriteLine("Please Enter a Maturity Rating\n" +
                          "1.  G\n" +
                          "2.  PG\n" +
                          "3.  PG 13\n" +
                          "4.  R\n" +
                          "5.  NC 17\n" +
                          "6.  TV Y\n" +
                          "7.  TV G\n" +
                          "8.  TV PG\n" +
                          "9.  TV 14\n" +
                          "10. TV MA\n");

        string maturityRating = Console.ReadLine()!;
        switch (maturityRating)
        {
            case "1":
                content.MaturityRating = MaturityRating.G;
                break;
            case "2":
                content.MaturityRating = MaturityRating.PG;
                break;
            case "3":
                content.MaturityRating = MaturityRating.PG_13;
                break;
            case "4":
                content.MaturityRating = MaturityRating.R;
                break;
            case "5":
                content.MaturityRating = MaturityRating.NC_17;
                break;
            case "6":
                content.MaturityRating = MaturityRating.TV_Y;
                break;
            case "7":
                content.MaturityRating = MaturityRating.TV_G;
                break;
            case "8":
                content.MaturityRating = MaturityRating.TV_PG;
                break;
            case "9":
                content.MaturityRating = MaturityRating.TV_14;
                break;
            case "10":
                content.MaturityRating = MaturityRating.TV_MA;
                break;
        }

        //* Genre
        Console.WriteLine("Select A Genre\n" +
            "1. Horror\n" +
            "2. RomCom\n" +
            "3. SciFi\n" +
            "4. Documentary\n" +
            "5. Bromance\n" +
            "6. Drama\n" +
            "7. Action\n");

        string genreInput = Console.ReadLine()!;

        //* give the number value for whatever the user input
        int genreId = int.Parse(genreInput);

        //* Conversion from int to enum
        content.GenreType = (GenreType)genreId;

        //* Ask user what kind of content is being created:
        Console.WriteLine("What kind of content is this?\n" +
                          "1. Movie\n" +
                          "2. Show\n");

        var userInputType = Console.ReadLine()!;

        switch (userInputType)
        {
            case "1":
                Console.WriteLine("You chose the Movie Type.");

                return new Movie
                {
                    Title = content.Title,
                    Description = content.Description,
                    StarRating = content.StarRating,
                    MaturityRating = content.MaturityRating,
                    GenreType = content.GenreType
                };

            case "2":
                Console.WriteLine("You chose the Show Type.");

                var theShow = new Show
                {
                    Title = content.Title,
                    Description = content.Description,
                    StarRating = content.StarRating,
                    MaturityRating = content.MaturityRating,
                    GenreType = content.GenreType
                };

                System.Console.WriteLine("Are there any Episodes?");
                var episode = new Episode();
                Console.WriteLine("Episode Title:");
                var userInputEpisodeTitle = Console.ReadLine()!;
                episode.Title = userInputEpisodeTitle;

                theShow.Episodes.Add(episode);

                return theShow;

            default:
                return content;
        }

    }

    private void RemoveContentFromList()
    {
        Console.Clear();
        Console.WriteLine("Which Item do you want to remove?");

        List<StreamingContentEntity> contentList = new List<StreamingContentEntity>();

        if (contentList.Count > 0)
        {
            #region Just Putting stuff on the screen and numbering it.
            int count = 0;

            foreach (StreamingContentEntity content in contentList)
            {
                count++;
                Console.WriteLine($"{count}. {content.Title}");
            }
            #endregion

            int targetContentId = int.Parse(Console.ReadLine()!);
            int targetIndex = targetContentId - 1;

            if (targetIndex >= 0 && targetIndex < contentList.Count)
            {
                StreamingContentEntity desiredContent = contentList[targetIndex];

                if(_repo.DeleteExistingContent(desiredContent))
                    Console.WriteLine($"{desiredContent.Title} was Successfully Deleted.");
                else
                    Console.WriteLine($"{desiredContent.Title} was Failed to be Deleted.");
            }
            else
            {
                Console.WriteLine("Invalid Id selection.");
            }

        }
        else
        {
            Console.WriteLine("There is no available content.");
        }

        PressAnyKey();
    }


    private bool CloseApplication()
    {
        System.Console.WriteLine("Thank you for using Streaming Content!");
        PressAnyKey();
        Console.Clear();
        return false;
    }

    private void PressAnyKey()
    {
        System.Console.WriteLine("Press Any Key to continue...");
        Console.ReadKey();
    }

    private void SeedContentList()
    {
        StreamingContentEntity rubber = new StreamingContentEntity("Rubber", "Tyre comes to life and kills people", 5, MaturityRating.R, GenreType.Drama);
        StreamingContentEntity toyStory = new StreamingContentEntity("Toy Story", "Best childhood movie.", 10, MaturityRating.G, GenreType.Bromance);
        StreamingContentEntity starWars = new StreamingContentEntity("Star Wars", "Jar Jar saves the world", 9.2, MaturityRating.PG_13, GenreType.Scifi);

        _repo.AddContentToDb(rubber);
        _repo.AddContentToDb(toyStory);
        _repo.AddContentToDb(starWars);
    }
}
