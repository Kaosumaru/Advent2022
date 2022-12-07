namespace Day7
{
    internal class Directory
    {
        public Directory(string name, Directory? parent = null)
        {
            _name = name;

            if (parent != null)
                _parent = new(parent);
        }

        public int TotalSize()
        {
            // TODO isn't invalidated, 
            if (_cachedTotalSize != -1)
                return _cachedTotalSize;
            _cachedTotalSize = _fileSizes + SubdirectoriesDict.Select(entry => entry.Value.TotalSize()).Sum();
            return _cachedTotalSize;
        }

        public IEnumerable<Directory> GetSubdirectories()
        {
            foreach (var entry in SubdirectoriesDict)
            {
                var dir = entry.Value;
                yield return dir;

                foreach (var subdir in dir.GetSubdirectories())
                    yield return subdir;
            }
        }

        public bool AddFile(File file)
        {
            if (FilesDict.ContainsKey(file.Name))
                return false;

            FilesDict[file.Name] = file;
            _fileSizes += file.Size;
            return true;
        }

        public Directory? GetParentDirectory()
        {
            if (_parent != null && _parent.TryGetTarget(out var p))
                return p;
            return null;
        }

        public Directory? CreateOrGetSubdirectory(string name)
        {
            var dict = GetSubdirectory(name);
            if (dict != null)
                return dict;
            return CreateSubdirectory(name);
        }

        public Directory? GetSubdirectory(string name)
        {
            SubdirectoriesDict.TryGetValue(name, out Directory? directory);
            return directory;
        }

        public Directory? CreateSubdirectory(string name)
        {
            if (SubdirectoriesDict.ContainsKey(name))
                return null;

            Directory d = new(name, this);
            SubdirectoriesDict[name] = d;
            return d;
        }

        string _name;
        List<Directory> Subdirectories = new();
        Dictionary<string, Directory> SubdirectoriesDict = new();

        Dictionary<string, File> FilesDict = new();
        WeakReference<Directory>? _parent;
        int _fileSizes = 0;
        int _cachedTotalSize = -1;
    }
}
