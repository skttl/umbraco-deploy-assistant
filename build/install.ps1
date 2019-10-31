param($installPath, $toolsPath, $package, $project)

$appPluginsFolder = $project.ProjectItems | Where-Object { $_.Name -eq "App_Plugins" }
$deployAssistantFolder = $appPluginsFolder.ProjectItems | Where-Object { $_.Name -eq "DeployAssistant" }

if (!$deployAssistantFolder)
{
	$newPackageFiles = "$installPath\Content\App_Plugins\DeployAssistant"

	$projFile = Get-Item ($project.FullName)
	$projDirectory = $projFile.DirectoryName
	$projectPath = Join-Path $projDirectory -ChildPath "App_Plugins"
	$projectPathExists = Test-Path $projectPath

	if ($projectPathExists) {
		Write-Host "Updating Deploy Assistant App_Plugin files using PS as they have been excluded from the project"
		Copy-Item $newPackageFiles $projectPath -Recurse -Force
	}
}
