using VRCFaceTracking.Core.SDK.v2.Facade;

namespace VRCFaceTracking.Core.SDK.v2;

public abstract class VrcftModuleV2
{
    public readonly IModuleController Module;

    /// <summary>
    /// If an exception occurs in this constructor, the module will not be loaded.
    /// </summary>
    /// <param name="controller">Allows you to control VRCFT.</param>
    protected VrcftModuleV2(IModuleController controller)
    {
        Module = controller;
    }

    /// <summary>
    /// The method will be called when this module is stopped, there are no conflicts with other modules,
    /// and this module should be enabled.<br /><br />
    /// The thread in which this method is run can be safely blocked until you decide that the module
    /// should be turned off.<br />
    /// When VRCFT needs to stop the method, it will set a flag in the
    /// CancellationToken and call Thread#Interrupt().<br />
    /// </summary>
    /// <param name="cancellationToken"> is a cancellation token that may be triggered
    /// at any time if the module needs to be stopped. This is always new object on each call.</param>
    public abstract void StartModule(CancellationToken cancellationToken);

    /// <summary>
    /// Called when VRCFT is shutting down. Use this method to clean up resources like threads.<br />
    /// It's crucial not to block this method, as doing so will prevent VRCFT from shutting down properly.<br />
    /// Any exceptions thrown will be ignored.<br />
    /// </summary>
    public abstract void Shutdown();
}
