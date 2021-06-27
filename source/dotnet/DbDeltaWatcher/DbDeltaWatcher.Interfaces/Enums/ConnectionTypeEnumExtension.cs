using System;

namespace DbDeltaWatcher.Interfaces.Enums
{
    public static class ConnectionTypeEnumExtension
    {
        /// <summary>
        /// Convert an int to a connection type
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static ConnectionTypeEnum AsConnectionType(this int i)
        {
            return i switch
            {
                1 => ConnectionTypeEnum.SqlServer,
                2 => ConnectionTypeEnum.MySql,
                _ => throw new ArgumentOutOfRangeException(nameof(i))
            };
        }

        /// <summary>
        /// Convert a ConnectionTypeEnum to an int
        /// </summary>
        /// <param name="connectionTypeEnum"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int ToInt(this ConnectionTypeEnum connectionTypeEnum)
        {
            return connectionTypeEnum switch
            {
                ConnectionTypeEnum.SqlServer => 1,
                ConnectionTypeEnum.MySql => 2,
                _ => throw new ArgumentOutOfRangeException(nameof(connectionTypeEnum), connectionTypeEnum, null)
            };
        } 
    }
}