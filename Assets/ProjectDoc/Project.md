# C# Naming Conventions

    ```csharp
    // Constants : UpperCase SnakeCase
    public const int CONSTANT_FIELD = 56;

    // Properties: PascalCase
    public static MyCodeStyle Instance {get; private set;}

    // Events: PascelCase
    public event EventHandler OnSomethingHappened;

    // Field: camelCase
    private float memberVariable;

    // Function Names: PascalCase
    private void Awake()
    {
        Instance = this;
        DoSomething(10f);
    }

    // Funcation Parans: camelCase
    private void DoSomething(float time)
    {
        // Do Something ..
        memberVariable = time + Time.deltaTime;
        if(memberVariable > 0)
        {
            // Do something else....
        }
    }
    ```

     
