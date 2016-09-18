param($installPath, $toolsPath, $package, $project)

$countryCodes = $project.ProjectItems.Item("CountryCodes.txt")
# set 'Copy To Output Directory' to 'Copy if newer'
$copyToOutput = $countryCodes.Properties.Item("CopyToOutputDirectory")
$copyToOutput.Value = 2