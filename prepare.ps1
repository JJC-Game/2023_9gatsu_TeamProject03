$shellPath = Get-Location
$shellParent = Split-Path $shellPath -Leaf
[System.Environment]::SetEnvironmentVariable("PROJECT_ROOT", $shellPath, "Machine")

Function CreateFolder($folderPath) {
    if( Test-Path $folderPath ){

    }else{
        New-Item $folderPath -ItemType Directory
    }
}

$dirPath = Join-Path $shellPath "\Assets\Scripts"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Resources"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Resources\FixData"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Prefabs"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Prefabs\System"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Prefabs\UI"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Prefabs\Chara"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Prefabs\Environment"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Prefabs\Effect"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Member"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Member\MemberScenes"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Member\MemberPrefabs"
CreateFolder $dirPath
$dirPath = Join-Path $shellPath "\Assets\Member\MemberScripts"
CreateFolder $dirPath

$projectAssetsDirPath = Join-Path $shellPath "\Assets\Resources\ProjectAssets"
CreateFolder $projectAssetsDirPath

[System.Environment]::SetEnvironmentVariable("PROJECT_ASSETS_LOCAL_DIR", $projectAssetsDirPath, "Machine")
[System.Environment]::SetEnvironmentVariable("PROJECT_ASSETS_SERVER_DIR", "\\201b05\ShareFolder\2023\$shellParent\ProjectAssets", "Machine")
