$clientClassPath=$args[0]
$utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
$newConstructor = "super(undefined);`n`t`tif (data) {`n`t`t`tfor (var property in data) {`n`t`t`t`tif (data.hasOwnProperty(property))`n`t`t`t`t`t(<any>this)[property] = (<any>data)[property];`n`t`t`t}`n`t`t}"

$newContent = (Get-Content $clientClassPath -encoding UTF8) -replace "super\(data\)\;", "$newConstructor"
$Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
[System.IO.File]::WriteAllLines($clientClassPath, $newContent, $utf8NoBomEncoding)
