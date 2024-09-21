using System.Runtime.Serialization;

public class LinqQueries
{
    private List<Film> FilmsCollection = new List<Film>();

    public LinqQueries()
    {
        using(StreamReader reader = new StreamReader("films.json"))
        {
            string json = reader.ReadToEnd();
            this.FilmsCollection = System.Text.Json.JsonSerializer.Deserialize<List<Film>>(json, new System.Text.Json.JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
        }
    }

    public IEnumerable<Film> AllCollection()
    {
        return FilmsCollection;
    }
    //Using order by
    public IEnumerable<Film> AllCollectionOrderedByTitle()
    {
        return FilmsCollection.OrderBy(item => item.Title);
    }
    //using where
    public IEnumerable<Film> FilmsFromXYear(int year)
    {
        return FilmsCollection.Where(item => item.ReleaseDate.Year == year);
    }
    //Using Any
    public bool IsThereAnyXFilm(string categorie)
    {
        return FilmsCollection.Any(item => item.Categories.Any(item => item.ToLower() == categorie.ToLower()));
    }
    //Finding films of a specific categorie and returning them using Where and any
    public IEnumerable<Film> FilmsOfXCategory(string category)
    {
        return FilmsCollection.Where(item => item.Categories.Any(item => item.ToLower() == category.ToLower()));

        /*Contains method doesn't work good enough with arrays as it is not possible to set up the sort of comparison we want to, so, 
        the best option is to use the Any method
        */
        //return FilmsCollection.Where(item => item.Categories.Contains(category));
    }
    //Are all the films from X year onwards? using All
    public bool AreAllfilmsFromXYearOn(int year)
    {
        return FilmsCollection.All(item => item.ReleaseDate.Year >= year);
    }
    //Finding the oldest movie using MinBy
    public Film OldestFilm()
    {
        return FilmsCollection.MinBy(item => item.ReleaseDate);
    }
    //Finding the newest movie using MaxBy
    public Film  NewestFilm()
    {
        return FilmsCollection.MaxBy(item => item.ReleaseDate);
    }
    //ordering the films by date in descending order 
    public IEnumerable<Film> OrderedFilmsByDateDesc()
    {
        return FilmsCollection.OrderByDescending(item => item.ReleaseDate);
    }
    /*
    Using TakeWhile
    order the films by date in ascending order and then, 
    return solely the films before an specific year
    */
    public IEnumerable<Film> FilmsBeforeXYear(int year)
    {
        return FilmsCollection
                .OrderBy(item => item.ReleaseDate)
                .TakeWhile(item => item.ReleaseDate.Year <= year);
    }
    /*
    Using SkipWhile
    Order the films by their title in descending order and then, 
    skip the films that start with a certain amount of letters of the alphabet
    */
    public IEnumerable<Film> FilmsStartsByASpecificLetters(string letters)
    {
        return FilmsCollection
                .OrderByDescending(item => item.Title)
                .SkipWhile(item => letters.Contains(item.Title[0], StringComparison.OrdinalIgnoreCase));
    }
    //Counting the films that belong to a specific category
    public int CountFilmsOfXCategory(string category)
    {
        return FilmsCollection.Count(item => item.Categories.Any(item => item.ToLower() == category.ToLower()));
    }
    //Concatenating the title films of a specific director using aggregate
    public string FilmsTitlesOfXDirector(string director)
    {
        return FilmsCollection
                .Where(item => item.Director.Contains(director, StringComparison.OrdinalIgnoreCase))
                .Aggregate("", (FilmsTitles, next) => 
                {
                    if(FilmsTitles != string.Empty)
                        FilmsTitles += " - " + next.Title;
                    else
                        FilmsTitles += next.Title;
                        
                    return FilmsTitles;
                });
    }
    //Summing the total letters of the title
    public int TotalTitleLetters()
    {
        return FilmsCollection.Sum(item => item.Title.Length);
    }
    //Getting the average number of the main actor from a film
    public double AverageMainActors() 
    {
        return FilmsCollection.Average(item => item.MainActors.Length);
    }
    /*
    Using GroupBy
    Grouping films by director using GroupBy
    */
    public IEnumerable<IGrouping<string, Film>> FilmsGroupedByDirector()
    {
        return FilmsCollection.GroupBy(item => item.Director);
    }
    /*
    filtering films by date range and then grouping them by date
    */
    public IEnumerable<IGrouping<int, Film>> FilmsGroupedByDate(int year1, int year2)
    {
        return FilmsCollection
                .Where(item => item.ReleaseDate.Year >= year1 && item.ReleaseDate.Year < year2)
                .OrderBy(item => item.ReleaseDate)
                .GroupBy(item => item.ReleaseDate.Year);
    }
}