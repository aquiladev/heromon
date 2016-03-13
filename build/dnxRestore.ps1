function GlobalRestore() {
	param(
		[string]$solutionDir
	)
	
	Write-Host "===== RESTORING ====="
	$stopWatch = [System.Diagnostics.Stopwatch]::startNew()
	
	&{$branch;iex ((new-object net.webclient).DownloadString('https://raw.githubusercontent.com/aspnet/Home/dev/dnvminstall.ps1'))}
	
	$globalJson = Get-Content -Path $solutionDir\global.json -Raw -ErrorAction Ignore | ConvertFrom-Json -ErrorAction Ignore

	if($globalJson) {
		$dnxVersion = $globalJson.sdk.version
		$dnxRuntime = $globalJson.sdk.runtime
		$dnxArch = $globalJson.sdk.architecture
		$global:dnxRuntimeVersion = "dnx-$dnxRuntime-win-$dnxArch.$dnxVersion"
	} else {
		throw "Unable to locate global.json"
	}
	
	& $env:USERPROFILE\.dnx\bin\dnvm install -r $dnxRuntime -arch $dnxArch $dnxVersion -Persistent

	Get-ChildItem -Path $solutionDir -Filter project.json -Recurse | ForEach-Object { & dnu restore $_.FullName 2>1 --quiet }
	Write-Host "===== RESTORED ($($stopWatch.Elapsed.TotalMinutes)) ====="
}