var builder = DistributedApplication.CreateBuilder(args);

var gameMasterAPI = builder.AddProject<Projects.ARPS_GameMasterAPI>("arps-gamemasterapi");
var probabilityCalculatorAPI = builder.AddProject<Projects.ARPS_ProbabilityCalculatorAPI>("arps-probabilitycalculatorapi");
var worldDescriberAPI = builder.AddProject<Projects.ARPS_WorldDescriberAPI>("arps-worlddescriberapi");

/*
builder.AddProject<Projects.ARPS_Frontend>("ARPS-Frontend")
    .WithExternalHttpEndpoints()
    .WithReference(gameMasterAPI)
    .WithReference(probabilityCalculatorAPI)
    .WithReference(worldDescriberAPI);
*/

builder.AddProject<Projects.ARPS_Site>("arp-site")
    .WithExternalHttpEndpoints()
    .WithReference(gameMasterAPI)
    .WithReference(probabilityCalculatorAPI)
    .WithReference(worldDescriberAPI);

builder.Build().Run();
