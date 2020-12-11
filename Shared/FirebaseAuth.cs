namespace Zebble
{
    using System;
    using System.Threading;

    public static class FirebaseAuth
    {
        static readonly Lazy<IFirebaseAuth> impl = new(() => CreateInstance(), LazyThreadSafetyMode.PublicationOnly);

        public static bool IsSupported => impl.Value != null;

        public static IFirebaseAuth Current => impl.Value ?? throw NotImplementedInReferenceAssembly();

        static IFirebaseAuth CreateInstance()
        {
#if NETSTANDARD1_0 || NETSTANDARD2_0
            return null;
#else
#pragma warning disable IDE0022 // Use expression body for methods
            return new FirebaseAuthImpl();
#pragma warning restore IDE0022 // Use expression body for methods
#endif
        }

        static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("This functionality is not implemented in the portable version of this assembly. You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
}
