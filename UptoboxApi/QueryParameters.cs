using System;

namespace UptoboxApi
{
    /// <summary>
    /// Parameters used into the GET requests of the Uptobox api 
    /// </summary>
    public class QueryParameters
    {
        private string? _dir;
        public enum EDirSort { Asc, Desc }
        private uint _limit = 10;
        /// <summary>
        /// Uptobox path
        /// </summary>
        public string Path { get; set; } = "//";

        public string? FileCode { get; set; }

        /// <summary>
        /// Limit of elements to show, min is 1, max is 100 
        /// </summary>
        public uint Limit
        {
            get => _limit;
            set => _limit = Math.Clamp(value, 1, 100);
        }
        /// <summary>
        /// how many elements do you want skip
        /// </summary>
        public uint Offset;
        /// <summary>
        /// Sort the api result by ASC or DESC based the column name wrote in the OrderBy attribute
        /// </summary>
        /// <returns>"ASC" or "DESC"</returns>
        public string Dir() => _dir ?? EDirSort.Asc.ToString() ;

        /// <summary>
        /// Sort the api result by ASC or DESC 
        /// </summary>
        /// <param name="dirSort"></param>
        public void Dir(EDirSort dirSort) => _dir = dirSort.ToString();

        /// <summary>
        /// The search field column name (ex: file_name)
        /// </summary>
        public string? SearchField;
        
        /// <summary>
        /// select the column to apply the order (ex: file_name)
        /// </summary>
        public string OrderBy = "file_name";
        
        /// <summary>
        /// Search content if searchField is provided
        /// </summary>
        public string? Search;

        public override string ToString()
        {
            string formattedText =
                $"\n\tLimit: {Limit},\n\tOffset: {Offset},\n\tDir: {Dir()},\n\tOrderBy: {OrderBy},\n\tPath: {Path},\n\tSearchField: {SearchField},\n\tSearch: {Search}\n";
            return string.Concat("{", formattedText, "}");
        }
    }
}