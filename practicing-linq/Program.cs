// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

LinqQueries queries = new LinqQueries();

//All collection
//PrintValues(queries.AllCollection());

//All collection ordered by title
//PrintValues(queries.AllCollectionOrderedByTitle());

//Films from X year
//PrintValues(queries.FilmsFromXYear(2002));

//Is there any film of a specific categorie?
//Console.WriteLine(queries.IsThereAnyXFilm("terror"));

//Returning films from a specific categorie
//PrintValues(queries.FilmsOfXCategory("Action"));

//Are all the films from X year onwards?
//int year = 1974;
//Console.WriteLine($"are all the films from {year} onwards?: {queries.AreAllfilmsFromXYearOn(year)}");


//Finding the oldest movie using MinBy
//var oldestFilm = queries.OldestFilm();
//Console.WriteLine($"{oldestFilm.Title}, {oldestFilm.ReleaseDate.ToShortDateString()}");

//Finding the newest movie using MaxBy
//var newestFilm = queries.NewestFilm();
//Console.WriteLine($"{newestFilm.Title}, {newestFilm.ReleaseDate.ToShortDateString()}");

//ordering the films by date in descending order 
//PrintValues(queries.OrderedFilmsByDateDesc());

//Taking films before x year by using where and take while
//PrintValues(queries.FilmsBeforeXYear(2001));

//Skiping films with some letter at the beginning of their title
//PrintValues(queries.FilmsStartsByASpecificLetters("zywuts"));

//Counting the films that belong to a specific category
//var categoryCount = queries.CountFilmsOfXCategory("crime");
//Console.WriteLine(categoryCount);

//Returning the titles films from a specific director
//var titlesFilmsOfXDirector = queries.FilmsTitlesOfXDirector("Christopher Nolan");
//Console.WriteLine(titlesFilmsOfXDirector);

//var totalTitleLetters = queries.TotalTitleLetters();
//Console.WriteLine(totalTitleLetters);

//var averageMainActors = queries.AverageMainActors();
//Console.WriteLine(averageMainActors);

//grouping films by director
//PrintGroups(queries.FilmsGroupedByDirector());

//filtering films by date range and then grouping them by date
PrintGroups(queries.FilmsGroupedByDate(1999, 2019));


void PrintValues(IEnumerable<Film> filmList)
{   //the minus number in the first formatting parameter means that the text will be justified to the left
    Console.WriteLine("{0,-60}, {1, 15}, {2, 11}, {3, 11}\n", "Title", "Release Year", "Categories", "director");
    foreach(var item in filmList)
    {
        Console.WriteLine("{0,-60}, {1, 15}, {2, 11}, {3, 11}", item.Title, item.ReleaseDate.ToShortDateString(), item.Categories[0], item.Director);
    }
}

void PrintGroups<TKey>(IEnumerable<IGrouping<TKey, Film>> groupList)
{
    foreach(var group in groupList)
    {
        Console.WriteLine("");
        Console.WriteLine($"Group: {group.Key}");
        Console.WriteLine("{0, -45} {1, 15} {2, 15} {3, 11}\n", "Title", "ReleaseDate", "Categories", "Director");
        foreach(var item in group)
        {
            Console.WriteLine("{0,-45}, {1, 15}, {2, 11}, {3, 11}\n", item.Title, item.ReleaseDate.ToShortDateString(), item.Categories[0], item.Director);
        }
    }
}