using System.Management.Automation;

namespace MongoBackupService;

public class CommonUtil
{
  /// <summary>
  /// Runs a PowerShell script with parameters and prints the resulting pipeline objects to the console output. 
  /// </summary>
  /// <param name="scriptContents">The script file contents.</param>
  /// <param name="scriptParameters">A dictionary of parameter names and parameter values.</param>
  public static async Task RunScript(string scriptContents)
  {
    // create a new hosted PowerShell instance using the default runspace.
    // wrap in a using statement to ensure resources are cleaned up.
   
    using (PowerShell ps = PowerShell.Create())
    {
      // specify the script code to run.
      ps.AddScript(scriptContents);
     
      // execute the script and await the result.
      var pipelineObjects = await ps.InvokeAsync().ConfigureAwait(false);
     
      // print the resulting pipeline objects to the console.
      foreach (var item in pipelineObjects)
      {
        Console.WriteLine(item.BaseObject.ToString());
      }
    }
  }

  public void CreateDirectory(string path)
  {
    Directory.CreateDirectory(path);
  }
}