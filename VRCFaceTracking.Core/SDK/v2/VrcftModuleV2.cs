#nullable enable
using VRCFaceTracking.Core.SDK.v2.Facade;

namespace VRCFaceTracking.Core.SDK.v2;

public abstract class VrcftModuleV2 : IEquatable<VrcftModuleV2>
{
    public readonly IModuleController Module;
    private readonly Lazy<Guid> _moduleId;

    /// <summary>
    /// If an exception occurs in this constructor, the module will not be loaded.
    /// </summary>
    /// <param name="controller">Allows you to control VRCFT.</param>
    protected VrcftModuleV2(IModuleController controller)
    {
        Module = controller;

        _moduleId = new Lazy<Guid>(GetModuleId, LazyThreadSafetyMode.ExecutionAndPublication);
    }

    /// <summary>
    /// Retrieves the cached unique identifier (GUID) for the module.
    /// This method provides an immutable property of the module's ID,
    /// ensuring that the ID is computed only once upon its first access
    /// and then consistently returned for subsequent calls.
    /// </summary>
    /// <returns>A <see cref="Guid"/> representing the unique identifier of the module.</returns>
    public Guid GetCachedModuleId() => _moduleId.Value;

    /// <summary>
    /// Abstract method to be implemented by derived classes to provide the unique identifier (GUID) for the module.
    /// This method is guaranteed to be called only once by VRCFT during the module's initialization.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Any exceptions thrown by this method, or a return value of <see cref="Guid.Empty"/> (or null if it were nullable),
    /// will prevent the module from being loaded by VRCFT.
    /// </para>
    /// <para>
    /// For subsequent access to the module's ID after initialization,
    /// always use the <see cref="GetCachedModuleId"/> method to retrieve the pre-computed and cached value,
    /// avoiding redundant computations and potential initialization failures.
    /// </para>
    /// </remarks>
    /// <returns>A <see cref="Guid"/> that uniquely identifies the module.</returns>
    protected abstract Guid GetModuleId();

    /// <summary>
    /// The method will be called when this module is stopped, there are no conflicts with other modules,
    /// and this module should be enabled.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The thread on which this method runs is a daemon thread.
    /// </para>
    /// <para>
    /// The thread in which this method is run can be safely blocked until you decide that the module
    /// should be turned off.
    /// </para>
    /// <para>
    /// When VRCFT needs to stop the method, it will set a flag in the CancellationToken and call Thread#Interrupt().
    /// </para>
    /// </remarks>
    /// <param name="cancellationToken"> is a cancellation token that may be triggered
    /// at any time if the module needs to be stopped. This is always new object on each call.</param>
    public abstract void StartModule(CancellationToken cancellationToken);

    /// <summary>
    /// Called when VRCFT is shutting down.
    /// This method should be used by derived classes to perform any necessary cleanup of resources,
    /// such as stopping background threads, releasing unmanaged resources, or saving state.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Any exceptions thrown within this method will be ignored by VRCFT to ensure the shutdown process
    /// can continue without interruption.
    /// </para>
    /// <para>
    /// VRCFT follows a specific shutdown sequence for modules:
    /// <list type="number">
    ///     <item>
    ///         <description>The <see cref="CancellationToken"/> passed to the <see cref="StartModule"/> method is signaled (set to true), and <c>Thread.Interrupt()</c> is called on the thread running <see cref="StartModule"/>.</description>
    ///     </item>
    ///     <item>
    ///         <description>A brief delay is introduced to allow the <see cref="StartModule"/> method to gracefully finish its operations.</description>
    ///     </item>
    ///     <item>
    ///         <description>This <see cref="Shutdown"/> method is then invoked. If this step does not lead to a complete shutdown, it indicates a critical issue preventing VRCFT from fully terminating.</description>
    ///     </item>
    /// </list>
    /// </para>
    /// </remarks>
    public abstract void Shutdown();

    public bool Equals(VrcftModuleV2? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return GetCachedModuleId().Equals(other.GetCachedModuleId());
    }

    public override sealed bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((VrcftModuleV2)obj);
    }

    public override sealed int GetHashCode() => GetCachedModuleId().GetHashCode();
}
