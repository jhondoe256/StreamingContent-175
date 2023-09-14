using StreamContent.Data.Entities;
using StreamContent.Data.Entities.Enums;
using System.Linq;

namespace StreamContent.Repository
{
    public class StreamContentRepository
    {
        //* So, we need a storage container that will 'hold' all of the streamcontent
        //* This is our 'fake-database' below (line 7)
        protected readonly List<StreamingContentEntity> _contentDb = new List<StreamingContentEntity>();

        //* 'The Repository Pattern' 
        //* C.R.U.D

        //* CREATE METHOD
        public bool AddContentToDb(StreamingContentEntity content)
        {
            int startingCount = _contentDb.Count;
            _contentDb.Add(content);

            if (_contentDb.Count > startingCount)
            {
                return true;
            }
            else
            {
                return false;
            }

            //todo:  bool wasAdded = (_contentDb.Count > startingCount) ? true : false;
            //todo:  return wasAdded;
        }

        //* READ METHOD
        public List<StreamingContentEntity> GetAllStreamingContent()
        {
            return _contentDb;
        }

        //* READ METHOD BY ID (TITLE)
        public StreamingContentEntity GetStreamingContentByTitle(string title)
        {
            //* 1 of many ways...
            foreach (StreamingContentEntity content in _contentDb)
            {
                if (content.Title == title)
                {
                    return content;
                }
            }
            return null;  //* sometimes it's good to just return a new StremingContentEntity();

            //* return _contentDb.SingleOrDefault(x=>x.Title == title);
        }

        //* UPDATE METHOD
        public bool UpdateExistingContent(string originalTitle, StreamingContentEntity updatedData)
        {
            StreamingContentEntity entityInDb = GetStreamingContentByTitle(originalTitle);

            //* Checking if the entity ACTUALLY exists!
            //* if its not equal to nothing...lets do something
            if (entityInDb != null)
            {
                entityInDb.Title = updatedData.Title;
                entityInDb.Description = updatedData.Description;
                entityInDb.GenreType = updatedData.GenreType;
                entityInDb.MaturityRating = updatedData.MaturityRating;
                entityInDb.StarRating = updatedData.StarRating;

                return true;
            }
            else
            {
                return false;
            }
        }
        //* DELETE METHOD

        public bool DeleteExistingContent(StreamingContentEntity content)
        {
            bool deleteResult = _contentDb.Remove(content);
            return deleteResult;
        }

        //todo: return List<StreamContentEntity> based on MaturityRating
        public List<StreamingContentEntity> GetContentByMaturityRating(MaturityRating maturityRating)
        {
            //* start off with an empty list
            List<StreamingContentEntity> aux = new List<StreamingContentEntity>();
          
            foreach (StreamingContentEntity item in _contentDb)
            {
                if (item.MaturityRating == maturityRating)
                {
                    aux.Add(item);
                }
            }

            return aux;
        }

    }
}