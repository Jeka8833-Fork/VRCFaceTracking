using VRCFaceTracking.Core.SDK.v2.Util;

namespace VRCFaceTracking.Core.SDK.v2.Core;

public record PacketHeader(Guid ModuleId, IReadOnlyWarpedMutableTime ExpireTime, IReadOnlyWarpedMutableTime Timestamp)
{
    public PacketHeaderBuilder Modify()
    {
        return new PacketHeaderBuilder(this);
    }

    /// <summary>
    /// A builder class for fluently constructing <see cref="PacketHeader"/> instances.
    /// </summary>
    public class PacketHeaderBuilder
    {
        private Guid _moduleId;
        private IReadOnlyWarpedMutableTime _expireTime;
        private IReadOnlyWarpedMutableTime _timestamp;

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketHeaderBuilder"/> class.
        /// Default values can be set here if desired, or properties can be left uninitialized
        /// to force the user to set them before calling Build().
        /// </summary>
        public PacketHeaderBuilder()
        {
            _moduleId = Guid.Empty; // A common default for Guids
        }

        public PacketHeaderBuilder(PacketHeader packetHeader)
        {
            _moduleId = packetHeader.ModuleId;
            _expireTime = packetHeader.ExpireTime;
            _timestamp = packetHeader.Timestamp;
        }

        /// <summary>
        /// Sets the ModuleId for the PacketHeader.
        /// </summary>
        /// <param name="moduleId">The unique identifier of the module.</param>
        /// <returns>The current builder instance for fluent chaining.</returns>
        public PacketHeaderBuilder WithModuleId(Guid moduleId)
        {
            _moduleId = moduleId;
            return this;
        }

        /// <summary>
        /// Sets the ExpireTime for the PacketHeader.
        /// </summary>
        /// <returns>The current builder instance for fluent chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if expireTime is null.</exception>
        public PacketHeaderBuilder WithExpireTime(IReadOnlyWarpedMutableTime expireTime)
        {
            _expireTime = expireTime ?? throw new ArgumentNullException(nameof(expireTime));
            return this;
        }

        /// <summary>
        /// Sets the Timestamp for the PacketHeader.
        /// </summary>
        /// <returns>The current builder instance for fluent chaining.</returns>
        /// <exception cref="ArgumentNullException">Thrown if timestamp is null.</exception>
        public PacketHeaderBuilder WithTimestamp(IReadOnlyWarpedMutableTime timestamp)
        {
            _timestamp = timestamp ?? throw new ArgumentNullException(nameof(timestamp));
            return this;
        }

        /// <summary>
        /// Builds a new <see cref="PacketHeader"/> instance with the configured values.
        /// </summary>
        /// <returns>A new <see cref="PacketHeader"/> instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown if any required fields are not set.</exception>
        public PacketHeader Build()
        {
            if (_moduleId == Guid.Empty)
            {
                throw new InvalidOperationException("ModuleId must be set before building PacketHeader.");
            }

            if (_expireTime == null)
            {
                throw new InvalidOperationException("ExpireTime must be set before building PacketHeader.");
            }

            if (_timestamp == null)
            {
                throw new InvalidOperationException("Timestamp must be set before building PacketHeader.");
            }

            return new PacketHeader(_moduleId, _expireTime, _timestamp);
        }
    }
}
