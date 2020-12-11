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


### Initialization
First of all, you need to initialize this plugin by calling `FirebaseAuth.Current.Initialize` method. To do so, make a call to it on your project startup, and feed it by your Firebase Web API key. It's recommended to store the key in your Zebble's `\Resources\Config.xml` file.

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


### API Usage
After initializing the plugin, you can use the following APIs.

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
This will send the provided credentials to Firebase and asks it to get an auth token. For calling this async method, you need to provide a pair of valid and existing email and password.

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

##### Note

Both of above methods will store the created or authenticated user info internally for further usage. 

#### RefreshTokenExpiry
It's obvious that the persisted user session will be expired after a period of time. So you should use this method to validate the persisted auth token. This will automatically revoke the token if it's already expired, otherwise the token expiration will be expanded. It's recommended to call this method prior to  your app launch. 

```csharp
class SplashPage : FullScreen
{
    protected async override Task NavigationStartedAsync()
    {
        var isValid = await FirebaseAuth.Current.RefreshTokenExpiry();

        if (isValid)
            Go<HomePage>();
        else
            Go<LoginPage>();
    }
}
```

#### GetUser
To get user details, call this method.

```csharp
class HomePage : FullScreen
{
    public async Task TapShowEmail()
    {
        var user = await FirebaseAuth.Current.GetUser();

        Dialog.Alert(`User email: {user.Email}`);
    }
}
```

#### IsAnonymous
To determine if user is anonymous, call this method.

```csharp
class HomePage : FullScreen
{
    public async Task TapGoToLoginIfRequired()
    {
        if (await FirebaseAuth.Current.IsAnonymous())
            Go<LoginPage>();
    }
}
```

#### IsAuthenticated
To determine if user is logged in, call this method.

```csharp
class HomePage : FullScreen
{
    public async Task TapSecurePage()
    {
        if (await FirebaseAuth.Current.IsAuthenticated())
            Go<SecurePage>();
    }
}
```

#### Logout
To log out the user, call this method. It will purge the internally stored state of the user.

```csharp
class HomePage : FullScreen
{
    public async Task TapLogout()
    {
        await FirebaseAuth.Current.Logout();
        
        Go<LoginPage>(PageTransition.SlideBack);
    }
}
```
