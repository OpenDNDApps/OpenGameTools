using System;
using System.Linq;
using System.Collections.Generic;

/* *****************************************************************************
 * File:    ComputerSizingExtensions.cs
 * Author:  Philip Pierce - Wednesday, October 01, 2014
 * Description:
 *  Extensions for sizing computer terms (KB, MB, GB, etc)
 *  
 * History:
 *  Wednesday, October 01, 2014 - Created
 * ****************************************************************************/

/// <summary>
/// Extensions for sizing computer terms (KB, MB, GB, etc)
/// </summary>
public static class ComputerSizingExtensions
{
    /// <summary>
    /// one kilobyte
    /// </summary>
    private const int INT_OneKB = 1024;

    /// <summary>
    /// Converts to kilobyte size
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int KB(this int value)
    {
        return value * INT_OneKB;
    }

    /// <summary>
    /// Converts to megabyte size (1024^2 bytes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int MB(this int value)
    {
        return value * INT_OneKB * INT_OneKB;
    }

    /// <summary>
    /// Converts to gigabyte size (1024^3 bytes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int GB(this int value)
    {
        return value * INT_OneKB * INT_OneKB * INT_OneKB;
    }

    /// <summary>
    /// Converts to terabyte size (1024^4 bytes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int TB(this int value)
    {
        return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
    }

    /// <summary>
    /// Converts to petabyte size (1024^5 bytes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int PB(this int value)
    {
        return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
    }

    /// <summary>
    /// Converts to exabyte size (1024^6 bytes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int EB(this int value)
    {
        return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
    }

    /// <summary>
    /// Converts to zettabyte size (1024^7 bytes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ZB(this int value)
    {
        return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
    }

    /// <summary>
    /// Converts to yottabyte size (1024^8 bytes)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int YB(this int value)
    {
        return value * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB * INT_OneKB;
    }
}