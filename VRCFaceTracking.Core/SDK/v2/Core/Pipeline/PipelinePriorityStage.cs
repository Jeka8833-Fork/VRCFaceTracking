namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline;

public static class PipelinePriorityStage
{
    public const int ModuleStage = 0;
    public const int MixerStage = 100;
    public const int FilterStage = 200;
    public const int CalibrationStage = 300;
    public const int ParameterGenerationStage = 400;

    public const int MaxPriority = 1;
    public const int NormalPriority = 50;
    public const int MinPriority = 99;
}
