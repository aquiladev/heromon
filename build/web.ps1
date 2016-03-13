param(
	[string]$task = "Build",
	[string]$configuration = "Release"
)

function Clean() {
	rd $outWebDir -recurse -force -ErrorAction SilentlyContinue | out-null
	mkdir $outWebDir -ErrorAction SilentlyContinue  | out-null
}

function Build() {	
	Clean
	GlobalRestore -solutionDir $solutionDir
	
	Write-Host "===== BUILDING ====="
	$stopWatch = [System.Diagnostics.Stopwatch]::startNew()
	
	dnu publish $solutionDir\src\Heromon.UI\project.json -o $outWebDir --runtime $dnxRuntimeVersion --configuration $configuration --quiet
	dnu publish $solutionDir\src\Heromon.Job\project.json -o $jobOutDir --runtime $dnxRuntimeVersion --configuration $configuration --quiet
	Write-Host "===== BUILDED ($($stopWatch.Elapsed.TotalMinutes)) ====="
}

function Publish() {
	Build
	
	Write-Host "===== PUBLISHING ====="
	$stopWatch = [System.Diagnostics.Stopwatch]::startNew()
	
	Select-AzureSubscription -SubscriptionId $azureSubscription
	. $currentDir\kuduSiteUpload.ps1 -websiteName $webApp -sourceDir $outWebDir -destinationPath "/site"
	Write-Host "===== PUBLISHED ($($stopWatch.Elapsed.TotalMinutes)) ====="
}

$currentDir = Split-Path -Parent $MyInvocation.MyCommand.Path;
$solutionDir = Split-Path $currentDir -Parent
$webApp = "heromon"
$outDir = "C:\pubs"
$outWebDir = "$outDir\$webApp"
$jobOutDir = "$outWebDir\wwwroot\App_Data\jobs\continuous\Heromon.Job"
$azureSubscription = "0a6b5c57-0d97-4086-809d-9404de631f56"

. $currentDir\dnxRestore.ps1

& $task 