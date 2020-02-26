# Core Result and Core client project

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

### Installation

Run Core Server

Install the dependencies

```sh
cd CoreServer
dotnet restore
```

Run Core Server

```sh
$ dotnet run
```

# Code Example

### First Set in Startup.cs

```sh
 public void ConfigureServices(IServiceCollection services)
        {
            //TODO change
            Rest client = Rest.Instanse("***Core Server url***", "*** Project Name ***",services);
            CoreState.Rest = client;
            CoreState.AddContextAccessor(services, "***Core Server url***", "***project name ***");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
 public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            CoreState.ContextMiddleware(app);
            app.UseMvc();
        }
```

### Secon In Controller

```sh
public class ValuesController:ControllerBase{
     private ICoreConfig _config;
      public ValuesController(
            ICoreConfig config)
        {

            _config = config;
        }
         [HttpGet]
        public NetResult<ResponseData> Get()
        {   // Get fromt Core Server joha config
           var Key= _config.ToString("joha");
           //Create automatic ResponseData in get Automatic 16 data from Core Server
            return 16;
         }
}
```
