namespace ValidarStream
{
    class ValidarString: IStream
    {
        private string _input;
        private int i;

        public ValidarString(string input)
        {
            _input = input;
            i = -1;
        }

        public char getNext()
        {
            i++;
            return _input[i];
        }

        public bool hasNext()
        {
            return (_input.Length <= (i + 1));
        }

        public string getText()
        {
            return _input.ToUpper();
        }
    }
}
