using System.Diagnostics;

namespace Day7
{
    internal class Parser : IDisposable
    {
        public Parser(string path)
        {
            _stream = new StreamReader(path);
            Parse();
        }

        public void Dispose()
        {
            _stream.Dispose();
        }

        void Parse()
        {
            while (true)
            {
                var line = _stream.ReadLine();
                if (line == null)
                    break;

                var s = line.Split(' ');
                Debug.Assert(s.Length > 0 && s[0][0] == prompt);
                var command = s[1];

                if (command == "ls")
                    Parse_ls();
                else if (command == "cd")
                    Parse_cd(s[2]);
                else
                    Debug.Assert(false);
            }
        }

        void Parse_cd(string name)
        {
            if (name == folderAbove)
            {
                Debug.Assert(_current != null);
                _current = _current.GetParentDirectory();
                return;
            }

            if (_current == null)
            {
                _root = new Directory(name);
                _current = _root;
                return;
            }

            _current = _current.CreateOrGetSubdirectory(name);
        }

        void Parse_ls()
        {
            Debug.Assert(_current != null);

            while (true)
            {
                if (_stream.Peek() == prompt)
                    break;
                var line = _stream.ReadLine();
                if (line == null)
                    return;

                var split = line.Split(' ');

                if (split[0] == "dir")
                {
                    _current.CreateSubdirectory(split[1]);
                }
                else
                {
                    _current.AddFile(File.Create(line));
                }
            }
        }

        public Directory Root()
        {
            Debug.Assert(_root != null);
            return _root;
        }

        const string folderAbove = "..";
        const char prompt = '$';
        Directory? _root;
        Directory? _current;
        StreamReader _stream;
    }
}
