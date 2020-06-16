
Write-Output "empty:"
Measure-Command { ./empty/bin/Release/netcoreapp3.1/publish/empty } | foreach { $_.TotalMilliseconds }

Write-Output "corefxlab:"
Measure-Command { ./corefxlab/bin/Release/netcoreapp3.1/publish/corefxlab -s Hello } | foreach { $_.TotalMilliseconds }

Write-Output "command-line-api:"
Measure-Command { ./command-line-api/bin/Release/netcoreapp3.1/publish/command-line-api -s Hello } | foreach { $_.TotalMilliseconds }

Write-Output "command-line-api-2:"
Measure-Command { ./command-line-api-2/bin/Release/netcoreapp3.1/publish/command-line-api-2 -s Hello } | foreach { $_.TotalMilliseconds }
