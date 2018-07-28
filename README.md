# PlayStationSharp
A C# library for the PlayStation API.

## Usage
__Notice:__
The `PlayStation.TestApp` project provides a more real life application of this library. You can use that as a reference if you so choose.

Due to changes to Sony's login system, you can no longer login with a username and password directly. Instead, this library utilizes their own login form and displays that to the user in a new form when called. This is how you can login using this library.

### Logging in
```csharp
using PlayStationSharp.API;

// Spawns a new Windows form with Sony's login form.
var account = Auth.CreateLogin();

// Will return null if form was closed manually.
if (account == null) return;

// 'account' variable now contains an instance of the user's account.
```
