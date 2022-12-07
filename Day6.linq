<Query Kind="Program">
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var inputFile = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "input.txt");

	var rootDirectory = new DirectoryObject(null, "/");
	var currentDirectory = rootDirectory;
	
	var consoleOutput = File.ReadAllLines(inputFile);
	foreach (var line in consoleOutput)
	{
		if (line.StartsWith('$')){
			ParseCommand(line);
		} else {
			ParseListing(line);
		}
	}
	
	// Part 1
	var dirsUnderSize = rootDirectory.GetDirectoryTree().Where(d => d.DirectorySize <= 100000).Sum(x => x.DirectorySize);
	Console.WriteLine($"Total Bytes of Directories <= 100K bytes: {dirsUnderSize}");
	
	// Part 2
	int freeSpaceNeeded = 30000000 - (70000000 - rootDirectory.DirectorySize);
	var dirToDelete = rootDirectory.GetDirectoryTree().Where(x => x.DirectorySize >= freeSpaceNeeded).OrderBy(x => x.DirectorySize).FirstOrDefault();
	Console.WriteLine($"Let's delete {dirToDelete.Name} with {dirToDelete.DirectorySize:n0}");
	
	// Local functions
	DirectoryObject ParseCommand(string line){

		var cmdParts = line.Split(' ');
		if (cmdParts[1] == "cd"){
			var newDir = cmdParts[2];
			if (newDir == "/"){
				currentDirectory = rootDirectory;
			} else if (newDir == ".."){
				currentDirectory = currentDirectory.Parent;
			} else {
				currentDirectory = currentDirectory.FindDirectory(newDir);
			}
		}		
		return currentDirectory;
	}
	
	void ParseListing(string line){
		var parts = line.Split(' ');
		if (parts[0] == "dir"){
			currentDirectory.CreateDirectoryIfNotExisting(parts[1]);
		} else {
			currentDirectory.CreateFileIfNotExisting(int.Parse(parts[0]), parts[1]);
		}
	}

}

// Our big beautiful class to represent a file or directory
// They have children of their own type and a parent of their own type so it 
// creates a tree structure
class DirectoryObject {

	private List<DirectoryObject> _childObjects;
	private string _name;
	private int _fileSize = 0;
	private bool _isDirectory = false;
	private DirectoryObject _parent = null;
	
	public string Name => _name;
	public bool IsDirectory => _isDirectory;
	public DirectoryObject Parent => _parent;
	
	public int DirectorySize => GetDirectorySize();
	
	public List<DirectoryObject> GetDirectoryTree(){

		var list = new List<DirectoryObject>();
		if (IsDirectory){
			list.Add(this);
			list.AddRange(_childObjects.Where(x => x.IsDirectory).SelectMany(x => x.GetDirectoryTree()));
			return list;
		}
		return null;
	}
	
	public DirectoryObject(DirectoryObject parent, string folderName)
	{
		_name = folderName;
		_childObjects = new List<DirectoryObject>();
		_isDirectory = true;
		_parent = parent;
	}
	
	public DirectoryObject(DirectoryObject parent, string fileName, int size)
	{
		_name = fileName;
		_fileSize = size;
		_parent = parent;
	}
	
	public void CreateDirectoryIfNotExisting(string name){
		var dir = _childObjects.SingleOrDefault(x  => x.Name == name && x.IsDirectory);
		if (dir == null){
			_childObjects.Add(new DirectoryObject(this, name));
		}
	}

	public void CreateFileIfNotExisting(int size, string name)
	{
		var file = _childObjects.SingleOrDefault(x => x.Name == name && !x.IsDirectory);
		if (file == null)
		{
			_childObjects.Add(new DirectoryObject(this, name, size));
		}
	}
	
	public DirectoryObject FindDirectory(string name){
		var dir = _childObjects.SingleOrDefault(x  => x.Name == name && x.IsDirectory);
		if (dir == null){
			throw new Exception("Directory not found!");
		} else {
			return dir;
		}
	}
	
	private int GetDirectorySize(){
		if (!this.IsDirectory){
			return _fileSize;
		}
		else
		{
			int size = 0;
			foreach (var obj in _childObjects)
			{
				size += obj.GetDirectorySize();
			}
			return size;
		}
	}
	
}