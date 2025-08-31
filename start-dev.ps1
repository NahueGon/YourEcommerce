wt.exe `
  powershell -NoExit -File "C:\projects\dotnet\YourEcommerce\YourEcommerceApi\start-api.ps1" `
  ; split-pane -V powershell -NoExit -File "C:\projects\dotnet\YourEcommerce\YourEcommerce\start-mvc.ps1" `
  ; split-pane -V powershell -NoExit -File "C:\projects\dotnet\YourEcommerce\tailwind\start-tailwind.ps1"
