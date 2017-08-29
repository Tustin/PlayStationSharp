# psn-csharp
A PSN API wrapper for C#.

## Usage
Add the DLL as a reference to your project. Then include the namespace at the very top of your project:
```csharp
using PSN;
```
Basic example for logging in:
```csharp
var acc = Auth.Login("my@email.com", "myp@55w0rd");
Console.WriteLine(user.Profile.onlineId);
```

Handling two-step authentication:
```csharp
Account Instance = null;
try
{
  Instance = Auth.Login("my@email.com", "myp@55w0rd");
}
catch (DualAuthSMSRequiredException ex)
{
  try
  {
    string ticket_uuid = ex.Message;

    string code = Microsoft.VisualBasic.Interaction.InputBox("Enter the code sent to your device:", "Two step code required");

    Instance = Auth.DualAuthLogin(code, ticket_uuid);
  }
  catch (Exception ex1)
  {
    MessageBox.Show(ex1.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
  }
}
catch (Exception ex)
{
  MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
}
```
