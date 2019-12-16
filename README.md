# MiniProfiler-ASP-NET-MVC-5
MiniProfiler package test with asp net mvc 5 - C#


Simple implementation of MiniProfiler for study.

Steps:

1 - Nuget Package MiniProfiler.MVC5 or run command Install-Package MiniProfiler.Mvc5

2 - Inside the Application_Start you have to add the following line:
    
    MiniProfiler.Configure(new MiniProfilerOptions());  
    
3 - Add the Application_BeginRequest and Application_EndRequest methods to start and stop the tracking at each request:
    
    protected void Application_BeginRequest()
    {
        MiniProfiler profiler = null;
        if (Request.IsLocal)
        {
            profiler = MiniProfiler.StartNew();
        }
    }

    protected void Application_EndRequest()
    {
        MiniProfiler.Current?.Stop();
    }
 
4 - In Shared/_Layout.cshtml master view. At the top, add the namespace using declaration @using StackExchange.Profiling;
    And at the bottom, just before the </body> code:
    
        @MiniProfiler.Current.RenderIncludes()
    </body>
    
5 - Configure the site to route all requests as managed code. Add the following line in the web.config.
    
    <system.webServer>
       <modules runAllManagedModulesForAllRequests="true" />
     </system.webServer>

6 - Configure to track MVC Controller. 
    Open the FilterConfig.cs file.
    Add this line to the RegisterGlobalFilters method:

    filters.Add(new ProfilingActionFilter());

8 - RUN!


