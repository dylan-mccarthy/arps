var builder = DistributedApplication.CreateBuilder(args);

var gameMasterAPI = builder.AddProject<Projects.ARPS_GameMasterAPI>("ARPS-GameMasterAPI");
var probabilityCalculatorAPI = builder.AddProject<Projects.ARPS_ProbabilityCalculatorAPI>("ARPS-ProbabilityCalculatorAPI");
var worldDescriberAPI = builder.AddProject<Projects.ARPS_WorldDescriberAPI>("ARPS-WorldDescriberAPI");

/*
builder.AddProject<Projects.ARPS_Frontend>("ARPS-Frontend")
    .WithExternalHttpEndpoints()
    .WithReference(gameMasterAPI)
    .WithReference(probabilityCalculatorAPI)
    .WithReference(worldDescriberAPI);
*/

builder.AddProject<Projects.ARPS_Site>("ARPS-Site")
    .WithExternalHttpEndpoints()
    .WithReference(gameMasterAPI)
    .WithReference(probabilityCalculatorAPI)
    .WithReference(worldDescriberAPI);

builder.Build().Run();
