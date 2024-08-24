# Define the path to the text file
$filePath = "nswag.json"

# Read the content of the file
$content = Get-Content -Path $filePath

# Define the old and new lines
$oldLine = '      "operationGenerationMode": "MultipleClientsFromOperationId",'
$newLine = '      "operationGenerationMode": "MultipleClientsFromFirstTagAndOperationName",'

# Replace the old line with the new line
$content = $content -replace [regex]::Escape($oldLine), $newLine

# Write the updated content back to the file
Set-Content -Path $filePath -Value $content