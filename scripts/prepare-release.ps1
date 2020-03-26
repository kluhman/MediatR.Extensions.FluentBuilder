$toolInstalled = (dotnet tool list -g | Select-String "nbgv")

if (!$toolInstalled) {
    Write-Host "Installing Nerdbank GitVersion CLI"
    dotnet tool install -g nbgv
}

cd ../src

$output = (nbgv prepare-release $args) | Out-String
Write-Host $output

[regex]$regex = "(?<releaseBranch>.*) branch now tracks (.*) stabilization and release\."
$result = $regex.Match($output)
$releaseBranch =  $result.Groups["releaseBranch"].Value

git push origin $releaseBranch

cd ../scripts