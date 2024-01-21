namespace InteractMerge.Internal
{
    public class FileNameSplitter : ISplitter
    {
        private char _delimiter;

        public FileNameSplitter( char delimiter = ',')
        {
            _delimiter = delimiter;
        }

        public string [] Split(string input) {

            return input.Split(_delimiter);
        }
    }
}
