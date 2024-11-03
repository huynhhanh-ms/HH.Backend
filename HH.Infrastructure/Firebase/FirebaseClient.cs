using HH.Domain.Common;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;



namespace HH.Infrastructure.Firebase;

public class FirebaseClient
{
    private FirebaseAuth? _auth;
    private FirebaseApp? _app;
    private StorageClient? _storage;

    public FirebaseClient()
    {
    }

    private FirebaseApp App => _app ??= FirebaseApp.Create(new AppOptions
    {
        Credential = GoogleCredential.FromFile(AppConfig.FirebaseConfig.DefaultPath)
    });


    public FirebaseAuth FirebaseAuth
    {
        get
        {
            return _auth ??= FirebaseAuth.GetAuth(App);
        }

    }

    public StorageClient Storage
    {
        get
        {
            return _storage ??= StorageClient.Create(GoogleCredential.FromFile(AppConfig.FirebaseConfig.DefaultPath));
        }
    }

    #region Destructor
    private bool _isDisposed = false;

    public void Dispose(bool disposing)
    {
        if (_isDisposed)
            return;

        if (disposing)
        {
            _app?.Delete();
        }

        _isDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~FirebaseClient()
    {
        Dispose(false);
    }
    #endregion Destructor
}
