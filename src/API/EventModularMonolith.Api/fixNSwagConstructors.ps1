$clientClassPath=$args[0]
$utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
$newConstructor = "super(undefined);`n`t`tif (data) {`n`t`t`tfor (var property in data) {`n`t`t`t`tif (data.hasOwnProperty(property))`n`t`t`t`t`t(<any>this)[property] = (<any>data)[property];`n`t`t`t}`n`t`t}"

$newContent = (Get-Content $clientClassPath -encoding UTF8) -replace "super\(data\)\;", "$newConstructor"
$newContent = $newContent -replace "uploadManyFiles\(myFiles\?\: any\[\] \| null \| undefined", "uploadManyFiles(myFiles?: FileParameter[] | null | undefined"
$newContent = $newContent -replace 'myFiles\.forEach\(item_ => content_\.append\(\"myFiles\", item_\.toString\(\)\)\);', 'myFiles.forEach(item_ => content_.append("myFiles", item_.data, item_.fileName ?? "test"));'
$Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
[System.IO.File]::WriteAllLines($clientClassPath, $newContent, $utf8NoBomEncoding)
