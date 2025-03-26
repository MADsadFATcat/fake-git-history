using System.Diagnostics;
using System.Text;

const int rows = 7;
const int cols = 52;

var today = DateTime.Today;
var startDate = today.AddDays(-(int)today.DayOfWeek).AddDays(rows * cols * -1);

var phrase = new[]
{
    "                                                    ",
    "   XXX  X X       XX  X   X XXX XXX XXX X   X XXX   ",
    "   X   XXXXX     X  X X   X X   X   X X XX XX X     ",
    "   X    X X      XXXX X   X XX  XXX X X X X X XX    ",
    "   X   XXXXX     X  X X X X X     X X X X   X X     ",
    "   XXX  X X      X  X  X X  XXX XXX XXX X   X XXX   ",
    "                                                    "
};

var script = new StringBuilder();
script.AppendLine($"git clone \\\"{Environment.GetEnvironmentVariable("GIT_REPO")}\\\" repo \\");
script.AppendLine("&& cd repo \\");
script.AppendLine("&& git config user.name \\\"$(git log -n 1 --pretty=format:%an)\\\" \\");
script.AppendLine("&& git config user.email \\\"$(git log -n 1 --pretty=format:%ae)\\\" \\");
script.AppendLine("&& git reset --hard $(git log --grep='init' --format=%H) \\");

for (var c = 0; c < cols; c++)
{
    for (var r = 0; r < rows; r++)
    {
        if (phrase[r][c] != 'X')
        {
            var commitDate = startDate.AddDays(c * rows + r);
            var commitMessage = $"Commit for {commitDate:yyyy-MM-dd}";
            script.AppendLine($"&& git commit --allow-empty -m \\\"{commitMessage}\\\" --date \\\"{commitDate:yyyy-MM-ddT01:mm:ss}\\\" \\");
        }
    }
}

script.AppendLine("&& git push --force origin main");
RunCommand(script.ToString());

return;

static void RunCommand(string command)
{
    var processInfo = new ProcessStartInfo("/bin/bash", $"-c \"{command}\"")
    {
        RedirectStandardError = true,
        RedirectStandardOutput = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };

    using var process = new Process();
    process.StartInfo = processInfo;
    process.Start();
    process.WaitForExit();

    var output = process.StandardOutput.ReadToEnd();
    var error = process.StandardError.ReadToEnd();

    if (!string.IsNullOrEmpty(output)) 
        Console.WriteLine(output);

    if (!string.IsNullOrEmpty(error)) 
        Console.WriteLine("Error: " + error);
}
