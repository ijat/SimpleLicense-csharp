# SimpleLicense-csharp
Simple software licensing library to verify license file based on date

# Install 
 
 Get it from [Nuget](https://www.nuget.org/packages/SimpleLicense/).
 
## Example

### Generate license
```
// Create private key and saved it as main.lic
SimpleLicense myLicenseProvider = SimpleLicense.Init("main.lic");
// Generate new license with expiry date
// Repeat this steps for more license
myLicenseProvider.CreateNewLicense("test.lic", DateTime.Now);
```

### Verify license
```
// Client side to verify license
SimpleLicense clientLicenseProvider = new SimpleLicense();
// true if license file integrity hasnt changed or not expired 
bool isLicenseValid = clientLicenseProvider.VerifyLicense("test.lic");
```

