[logo]: https://raw.githubusercontent.com/Geeksltd/Zebble.FirebaseAuth/master/icon.png "Zebble.FirebaseAuth"


## Zebble

![logo]

A Zebble plugin for signing with Firebase Auth.


[![NuGet](https://img.shields.io/nuget/v/Zebble.FirebaseAuth.svg?label=NuGet)](https://www.nuget.org/packages/Zebble.FirebaseAuth/)

> With this plugin you can get information from the user of Firebase Auth like email in your Zebble application and it is available on all platforms.

<br>


### Setup
* Available on NuGet: [https://www.nuget.org/packages/Zebble.FirebaseAuth/](https://www.nuget.org/packages/Zebble.FirebaseAuth/)
* Install in your platform client projects.
* Available for iOS, Android and UWP.
<br>


### API Usage
First of all, you need to initialize this plugin by calling `FirebaseAuth.Current.Initialize` method on your project startup, to feed it by your Firebase Web API key. It's recommended to store the key in your Zebble's `\Resources\Config.xml` file.

```csharp
class StartUp
{
    public static Task Run()
    {
        FirebaseAuth.Current.Initialize(Config.Get("Firebase.ApiToken"));

        // TODO: Any required init
        Zebble.Mvvm.ViewModel.Go<WelcomePage>();
        return Task.CompletedTask;
    }
}
```
Then you can use the following APIs.

#### Register
This will create a new user account under your Firebase Auth app. For calling this async method, you need to provide a pair of email and password.

```csharp
class LoginPage : FullScreen
{
    public readonly Bindable<string> Email = new Bindable<string>();
    public readonly Bindable<string> Password = new Bindable<string>();

    public async Task TapLogin()
    {
        var result = await FirebaseAuth.Current.Register(Email.Value, Password.Value);

        if (result.Succeeded)
            Go<WelcomePage>();
        else
            Dialog.Alert($"Register failed: {result.Message} ({result.Code})");
    }
}
```

#### Login
This will create a new user account under your Firebase Auth app. For calling this async method, you need to provide a pair of email and password.

```csharp
class LoginPage : FullScreen
{
    public readonly Bindable<string> Email = new Bindable<string>();
    public readonly Bindable<string> Password = new Bindable<string>();

    public async Task TapLogin()
    {
        var result = await FirebaseAuth.Current.Login(Email.Value, Password.Value);

        if (result.Succeeded)
            Go<WelcomePage>();
        else
            Dialog.Alert($"Login failed: {result.Message} ({result.Code})");
    }
}
```


