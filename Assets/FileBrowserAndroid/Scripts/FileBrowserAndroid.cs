using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileBrowserAndroid : MonoBehaviour {

	public GameObject fileButton;
    public GameObject folderButton;
    public Text uriPathDisplay;

    private GameObject grid;
    private FileManager fileManager;

    private string root;
    private string currentPath;
    private Stack memory;

    private AndroidModule androidModule;
    public string[] availableExtensions;

    private void Awake()
    {
        this.androidModule = this.GetComponent<AndroidModule>();
        this.grid = this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        this.fileManager = GameObject.Find("FileManager").GetComponent<FileManager>();
        memory = new Stack();
    }

    private void Start()
    {
        root = androidModule.GetAndroidRootExternalStorage();
        currentPath = root;
        uriPathDisplay.text = TransformPathToDisplay(currentPath);
        BuildFilesAndDirectories();
    }

    private void BuildFilesAndDirectories()
    {
        string[] directories = Directory.GetDirectories(currentPath);
        string[] files = Directory.GetFiles(currentPath);

        foreach (string directory in directories)
        {
            string dirName = new DirectoryInfo(directory).Name;
            GameObject button = Instantiate(folderButton, this.grid.transform, false);
            var fileBrowserButton = button.GetComponent<FileBrowserButton>();
            fileBrowserButton.SetFileBrowser(this);
            fileBrowserButton.SetText(dirName);
            fileBrowserButton.SetAbsolutePath(directory);
            button.GetComponent<FolderButton>().ModifyImageIfContainsExtension(availableExtensions);
        }

        foreach (string file in files)
        {
            if (availableExtensions.Contains(Path.GetExtension(file)))
            {
                string fileName = Path.GetFileName(file);
                GameObject button = Instantiate(fileButton, this.grid.transform, false);
                var fileBrowserButton = button.GetComponent<FileBrowserButton>();
                fileBrowserButton.SetFileBrowser(this);
                fileBrowserButton.SetText(fileName);
                fileBrowserButton.SetText(fileName);
                fileBrowserButton.SetAbsolutePath(file);                
                button.GetComponent<FileButton>().SetImage(Path.GetExtension(file));
            }
        }
    }

    public void ReBuildFilesAndDirectories(string newPath)
    {
        memory.Push(currentPath);
        ClearFileBrowserContent();
        currentPath = newPath;
        uriPathDisplay.text = TransformPathToDisplay(currentPath);
        BuildFilesAndDirectories();
    }
    public void ReBuildFilesAndDirectoriesWithoutMemory(string newPath)
    {
        ClearFileBrowserContent();
        currentPath = newPath;
        uriPathDisplay.text = TransformPathToDisplay(currentPath);
        BuildFilesAndDirectories();
    }

    private string TransformPathToDisplay(string path)
    {
        if(path.Length > 70)
        {
            int mid = path.Length >> 1;
            path = path.Substring(path.Substring(mid).IndexOf('\\') + mid);
        }

        return path.ToLower();        
    }
    private int GetLastSlash(string path)
    {
        return path.LastIndexOf('\\');
    }

    public string GetBeforeDirectoryVisisted()
    {
        if (memory.Count == 0)
            return this.root;
        return memory.Pop() as string;
    }

    public void ClearFileBrowserContent()
    {
        foreach (Transform child in this.grid.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public FileManager GetFileManager()
    {
        return this.fileManager;
    }

    public string GetRootDirectory()
    {
        return this.root;
    }

    public string GetCurrentPath()
    {
        return this.currentPath;
    }
}
